using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour {

	public Transform respawnPoint;
	public GameObject player;
	public GameObject checkpointMover;
	// Use this for initialization	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		if(col.collider.tag == "KillingObject"){
			player.transform.position = respawnPoint.position;
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.collider.tag == "KillingObject"){
			player.transform.position = respawnPoint.position;
		}
		if(col.collider.gameObject == checkpointMover){
			respawnPoint.transform.position = checkpointMover.transform.position;
		}
	}



}
