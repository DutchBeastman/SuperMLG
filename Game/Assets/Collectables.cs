using UnityEngine;
using System.Collections;

public class Collectables : MonoBehaviour {
	GameObject[] pickUps;
	private static int totalAmountOfPickUps = 0;
	private static int currentAmountOfPickUps = 0;

	void Start () {
		pickUps = GameObject.FindGameObjectsWithTag("Item");
		totalAmountOfPickUps = pickUps.Length ;
		currentAmountOfPickUps = totalAmountOfPickUps - pickUps.Length;
		ChangeGUIText();
	}

	public void AddOneToCounter(){
		pickUps = GameObject.FindGameObjectsWithTag("Item");
		currentAmountOfPickUps = totalAmountOfPickUps - (pickUps.Length - 1);
		//Debug.Log("this adds one to current amount");
		ChangeGUIText();
	}
	void ChangeGUIText(){
		//Debug.Log("this updates the current amount in the text");
		//gameObject.guiText.text = "current amount:" + currentAmountOfPickUps;
		gameObject.guiText.text = totalAmountOfPickUps + " / " + currentAmountOfPickUps;
		Debug.Log(currentAmountOfPickUps);
		//Debug.Log(gameObject.guiText.text);

	}
	void Update(){
		
		gameObject.guiText.text = totalAmountOfPickUps + " / " + currentAmountOfPickUps;
	}
}
