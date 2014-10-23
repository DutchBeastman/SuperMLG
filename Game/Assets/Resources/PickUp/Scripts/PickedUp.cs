using UnityEngine;
using System.Collections;

public class PickedUp : MonoBehaviour {
	
	public GameObject collectables;															// gameobject with collectables script

	void OnCollisionEnter(Collision col){
		if(col.collider.tag == "Player")
			IGotPickedUp();
	}
	void IGotPickedUp(){
		Destroy(this.gameObject);
		collectables.GetComponent<Collectables>().AddOneToCounter();
	}
}
