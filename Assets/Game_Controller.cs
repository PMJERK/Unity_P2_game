using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game_Controller : MonoBehaviour {

	public static int coinAmount;
	public static int score; 
	public Text coinText;
	public Text scoreText; 
	public static bool finished; 
	public GameObject panel; 
	bool calculated; 

	// Use this for initialization
	void awake () {

		coinText.GetComponent<Text>();
		scoreText.GetComponent<Text>();
		coinAmount = 0;
		score = 0; 
	}
	
	// Update is called once per frame
	void Update () {

		ScoreManager();
			
	}

	void ScoreManager() {

		coinText.text = "" + coinAmount;

		if (finished && !calculated) {
			score = score + (2 * coinAmount * score)/100;

			scoreText.text = "Highscore: " + score;
			panel.SetActive (true);
			calculated = true;
		} else if (!calculated) {
			panel.SetActive (false); //For some reason this won't work in the awake method
		}
	}

}
