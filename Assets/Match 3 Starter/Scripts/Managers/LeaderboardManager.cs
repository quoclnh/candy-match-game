using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.IO;
using System;

public class LeaderboardManager : MonoBehaviour {
	[Serializable]
	public class Profile {
		public string name;
		public int score;

        public Profile(string v1, int v2) {
            name = v1;
            score = v2;
        }
    }

	public static GUIManager instance;

	public Text top1_name;
	public Text top2_name;
	public Text top3_name;
	public Text scoreTxt1;
	public Text scoreTxt2;
	public Text scoreTxt3;

	void Awake() {
		instance = GetComponent<GUIManager>();
    }

	public Profile getProfileByRank(int n) {
		HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://pruapihighscore.azurewebsites.net/Score/thun?n=" + n);

		try {
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			StreamReader reader = new StreamReader(response.GetResponseStream());
			string json = reader.ReadToEnd();
			return JsonUtility.FromJson<Profile>(json);
		} catch {
			return new Profile("---", 0);
        }
	}

    void Start() {
		Profile profile;

        profile = getProfileByRank(0);
		top1_name.text = profile.name;
		scoreTxt1.text = profile.score.ToString();

		profile = getProfileByRank(1);
		top2_name.text = profile.name;
		scoreTxt2.text = profile.score.ToString();

		profile = getProfileByRank(2);
		top3_name.text = profile.name;
		scoreTxt3.text = profile.score.ToString();
	}
}
