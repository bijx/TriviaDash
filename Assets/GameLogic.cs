using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour {

	public Text qnum,question,a1,a2,a3,a4,timerText, gameOverText, score, coinsText;
	public Button restart, back, b1,b2,b3,b4;
	int correctAnswers = 0;
	public GameObject check, x;
	private int questionNumber = 1;
	private int q;
	string[] questionBank = {
		"How many countries are there on earth?",
		"How many continents are there?",
		"What is the tallest building on earth?",
		"The square root of Pi squared is",
		"How many sides does a nonagon have?",
		"Who's murder sparked WWI?",
		"What is the integral of x^2?",
		"Roughly how many feet are in a meter?",
		"How high above earth is the international space station?",
		"What year did the Vietnam war begin?"
	};
	string[] answerOne = { "195", "9", "CN Tower", "Square root Pi", "6","Adolf Hitler","(x^2)/2","3","503km","1975"};
	string[] answerTwo = {"201","7","Taipei 101","Pi squared","7","Archduke Ferdinand","(x^3)/3","4","2055km","1955"};
	string[] answerThree = {"187","6","Burj Khalifa","Pi","8","Woodrow Wilson","x","5","5013km","1962"};
	string[] answerFour = {"170","8","Chrysler Building","Undefined","9","None of the above","2x","2","408km","1968"};
	int[] correctAnswer = { 1, 2, 3, 3, 4, 2, 2, 1, 4, 2 };
	int[] questionsAsked = { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
	float timeLeft = 20.0f;
	float increment;
	bool sameQuestion = true;
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("Difficulty") == 0) {
			increment = 20f;
		} else if (PlayerPrefs.GetInt ("Difficulty") == 1) {
			increment = 12f;
		} else {
			increment = 6f;
		}
		timeLeft = Time.deltaTime + increment;
		q = Random.Range (0, questionBank.Length-1);
		qnum.text = "Q"+questionNumber;
		question.text = questionBank [q];
		a1.text = answerOne[q];
		a2.text = answerTwo[q];
		a3.text = answerThree[q];
		a4.text = answerFour[q];
		questionsAsked [questionNumber] = q;
		questionNumber++;
	}



	public void CheckAnswer(int button){
		if (button == correctAnswer [q]) {
			PlayerPrefs.SetInt ("Coins", PlayerPrefs.GetInt ("Coins") + 5);
			correctAnswers++;
			Start ();
			StartCoroutine(showCheck (1));
		} else {
			StartCoroutine(showCheck (0));
			Start ();
		}
	}
		
	IEnumerator showCheck(int mark){ // 0 = x, 1 = check
		if (mark == 0) {
			x.SetActive (true);
			check.SetActive (false);
		} else if (mark == 1) {
			x.SetActive (false);
			check.SetActive (true);
		}
		yield return new WaitForSeconds (0.5f);
		x.SetActive (false);
		check.SetActive (false);
	}


	// Update is called once per frame
	void Update () {
		if (questionNumber ==7 && !gameOver) {
			GameOver (1);
		}
		if (!gameOver) {
			timeLeft -= Time.deltaTime;
			timerText.text = "" + Mathf.Round (timeLeft);
			coinsText.text = "" + PlayerPrefs.GetInt ("Coins", 0);
		}
		if(timeLeft < 0)
		{
			GameOver(0);
		}

		if (timeLeft < 5) {
			timerText.color = new Color (255, 0, 0);
		}else if (timeLeft < 10) {
			timerText.color = new Color (255, 100, 0);
		} else {
			timerText.color = new Color (255, 255, 255);
		}

	}

	bool gameOver = false;
	void GameOver(int state){ // state = 0 means time ran out, state = 1 means question limit was reached.
		gameOver = true;
		timerText.text = "0";

			a1.gameObject.SetActive (false);
			a2.gameObject.SetActive (false);
			a3.gameObject.SetActive (false);
			a4.gameObject.SetActive (false);
			b1.gameObject.SetActive (false);
			b2.gameObject.SetActive (false);
			b3.gameObject.SetActive (false);
			b4.gameObject.SetActive (false);
			qnum.gameObject.SetActive (false);
			question.gameObject.SetActive (false);
			gameOverText.gameObject.SetActive (true);
			score.gameObject.SetActive (true);
			score.text = "Correct Answers: " + correctAnswers + "/5";
			restart.gameObject.SetActive (true);
			back.gameObject.SetActive (true);

	}

	public void Restart(){
		SceneManager.LoadScene ("TheGame");
	}

	public void BackToMenu(){
		SceneManager.LoadScene ("MainMenu");
	}



}
