using UnityEngine;
using System.Collections;

public class PickedUp : MonoBehaviour {
	
	public GameObject collectables;

	void OnCollisionEnter(Collision col){
		if(col.collider.tag == "Player")
			IGotPickedUp();
	}
	void IGotPickedUp(){
		Debug.Log("this starts adding one to counter");
		Destroy(this.gameObject);
		collectables.GetComponent<Collectables>().AddOneToCounter();
	}
}
