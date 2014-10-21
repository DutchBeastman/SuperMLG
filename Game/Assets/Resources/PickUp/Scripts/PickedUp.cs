using UnityEngine;
using System.Collections;

public class PickedUp : MonoBehaviour {

	void OnCollisionEnter(Collision col){
		if(col.collider.tag == "Player")
			Destroy(this.gameObject);
	}
}
