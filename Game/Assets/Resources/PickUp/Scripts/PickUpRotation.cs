using UnityEngine;
using System.Collections;

public class PickUpRotation : MonoBehaviour {

	Vector3 rotationSpeed;
	void Start () {
		rotationSpeed.x = 0;
		rotationSpeed.z = 0;
	}

	void Update () {
		rotationSpeed.y = Random.Range(0, 360);
		transform.Rotate((rotationSpeed * Time.deltaTime)/4);
	}
}
