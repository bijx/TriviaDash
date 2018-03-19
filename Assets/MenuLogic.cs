using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour {

	public GameObject MenuButtons;
	public GameObject NewGameButtons;

	public GameObject bg;
	public float shakeRange = 5f;
	public RawImage Title;
	public Slider diffSlider;
	public Text diffText,coins;

	// Use this for initialization
	void Start () {
		coins.text = ""+PlayerPrefs.GetInt ("Coins",0);
	}
	
	// Update is called once per frame
	void Update () {
		bg.transform.Translate (new Vector2(0.25f,0));

		float scaleNum = Mathf.PingPong(Time.time*0.05f, 0.1f)+0.3f;
		Title.transform.localScale = new Vector3 (scaleNum,scaleNum);

		if (diffSlider.value == 0) {
			PlayerPrefs.SetInt ("Difficulty", 0);
			diffText.text = "Easy";
			float z = Random.value * 3f;
			diffText.transform.eulerAngles = new Vector3(diffText.transform.rotation.x, diffText.transform.rotation.y, diffText.transform.rotation.z + z); 
			diffText.color = new Color(167f/255.0f, 249f/255.0f, 35f/255.0f);
		} else if (diffSlider.value == 1) {
			PlayerPrefs.SetInt ("Difficulty", 1);
			diffText.text = "Medium";
			diffText.color = new Color(249f/255.0f, 217f/255.0f, 35/255.0f);
			float z = Random.value * 6f;
			diffText.transform.eulerAngles = new Vector3(diffText.transform.rotation.x, diffText.transform.rotation.y, diffText.transform.rotation.z + z); 
		}else if (diffSlider.value == 2) {
			PlayerPrefs.SetInt ("Difficulty", 2);
			diffText.text = "Hard";
			float z = Random.value * 12f;
			diffText.transform.eulerAngles = new Vector3(diffText.transform.rotation.x, diffText.transform.rotation.y, diffText.transform.rotation.z + z); 
			diffText.color = new Color(249f/255.0f, 35f/255.0f, 35/255.0f);

		}

	}

	public void NewGame(){
		NewGameButtons.SetActive (true);
		MenuButtons.SetActive (false);
	}

	public void CancelNewGame(){
		NewGameButtons.SetActive (false);
		MenuButtons.SetActive (true);
	}

	public void Quit(){
		Application.Quit ();	
	}

	public void Play(){
		Debug.Log (diffSlider.value);
		PlayerPrefs.SetInt ("Difficulty", Mathf.RoundToInt(diffSlider.value));
		SceneManager.LoadScene ("TheGame");
	}

	public void Shop(){
	}
}
