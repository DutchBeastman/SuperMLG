﻿using UnityEngine;
using System.Collections;

public class PlatformTrigger : MonoBehaviour {
		
		private bool isActive = false;
		private bool DetectObject = false;
		public Platform parentScript;
		
		void Start()
		{
			if(transform.parent.gameObject.GetComponent<Platform>() == null || transform.parent == null) {
				Debug.LogError("PingPongPlatform Script is not attached to" + transform.parent.gameObject.name);
				return;
			} else {
				parentScript = transform.parent.gameObject.GetComponent<Platform>();
			}
		}
		
		void OnTriggerEnter(Collider triggerObj)
		{
			if(parentScript.AutoMove == false) {
				if(isActive == false || triggerObj.gameObject.tag == "Player") {
					parentScript.HoldTimer = 0f;
					isActive = true;
				}
			}
			if(DetectObject == false) {
				DetectObject = true;
			}
		}
		
		void OnTriggerExit()
		{
			if(isActive == true || parentScript.AutoMove == false) {
				parentScript.PlatformDeactivate();
				isActive = false;
			}
			if(DetectObject == true) {
				parentScript.SnapPlayerToPlatform(false);
				DetectObject = false;
			}
		}
		
		void Update()
		{
			if(isActive == true || parentScript.AutoMove == true)
				parentScript.PlatformActivate();
			
			if(DetectObject == true) {
				parentScript.SnapPlayerToPlatform(true);
			}
		}
	}

