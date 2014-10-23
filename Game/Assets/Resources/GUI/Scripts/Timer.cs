using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	
	bool playing = true;
	float timeInGame;
	int min;

	void Start () {
		timeInGame = 0;
		min = 0;
		//StartCoroutine(Time);
	}
	
	void Update () {

		if(playing)
			timeInGame =  Mathf.Round(100) /20;					// Making sure i get 2 digits after whole seconds

		if(timeInGame > 60){}
			//AddMinutes();											// Set seconds to 0 and add a minute

		gameObject.guiText.text = "Time: " + timeInGame;
		//gameObject.guiText.text = "Time: " + min + ":" + timeInGame;
	}

	void AddMinutes(){
		Debug.Log("Before "+timeInGame);
		timeInGame -= 60;
		Debug.Log ("After "+timeInGame);
		min += 1;
	}
}
