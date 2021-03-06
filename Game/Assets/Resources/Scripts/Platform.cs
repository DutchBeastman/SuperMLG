﻿using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	public bool AutoMove = false;
	public Transform Target;
	public float Speed = 5f;
	public float Hold = 5f;
	public float HoldTimer { get; set;}
	
	private float lastFrame;
	private string lastAnimation;
	private string direction = "AtoB";
	private Animation MoveAnim;
	
	void Start() {
		
		if(Target == null)
			return;
		
		if (gameObject.GetComponent("Rigidbody") == null) {
			gameObject.AddComponent("Rigidbody");
			gameObject.rigidbody.isKinematic = true;
		}
		
		MoveAnim = gameObject.AddComponent("Animation") as Animation;
		MoveAnim.animatePhysics = true;
		// create animations for going to point and returning to orginal location
		GoToTarget(transform.localPosition, Target.localPosition, "AtoB");
		GoToTarget(Target.localPosition, transform.localPosition, "BtoA");
	}
	
	void OnDrawGizmosSelected() {
		if(Target != null) {
			Gizmos.color = Color.green;
			Gizmos.DrawLine(transform.position, Target.position);
			Gizmos.DrawWireCube(Target.position, new Vector3(0.2f, 0.2f, 0.2f));
		}
	}
	
	public void PlatformActivate() {
		// wait for the player to jump on and stay on for x seconds before moving
		// PlatformActivate is called by FloatPlatformTrigger
		if(animation.isPlaying == false) {
			if(HoldTimer < Hold) {
				HoldTimer += 1f * Time.deltaTime;
			} else if (direction == "AtoB") {
				//Debug.Log("JumpTo " + lastFrame.ToString());
				MoveAnim.Play("AtoB");
				HoldTimer = 0f;
				direction = "BtoA";
			} else if (direction == "BtoA") {
				//Debug.Log("JumpTo " + lastFrame.ToString());
				MoveAnim.Play("BtoA");
				HoldTimer = 0f;
				direction = "AtoB";
			}
		}
	}
	
	public void PlatformDeactivate() {
		// stop animation if player jumps off
		foreach (AnimationState state in animation) {
			if(state.clip.name == "AtoB") {
				lastFrame = animation["AtoB"].normalizedTime;
				lastAnimation = "AtoB";
				MoveAnim.Stop("AtoB");
				animation["AtoB"].normalizedTime = lastFrame;
			}
			
			if(state.clip.name == "BtoA") {
				lastFrame = animation["BtoA"].normalizedTime;
				lastAnimation = "BtoA";
				MoveAnim.Stop("BtoA");
				animation["BtoA"].normalizedTime = lastFrame;
			}
			
			// if the player jumps off the platform before it reaches the end resume the last animation
			if(direction != lastAnimation || lastFrame > 0) {
				direction = lastAnimation; 
			}
		}
		//Debug.Log(lastFrame.ToString() + ", " + lastAnimation);
	}
	
	public void SnapPlayerToPlatform(bool onPlatform) {
		// change move modifier on player
		if(onPlatform == true) {
			if(gameObject.rigidbody.velocity.y <= 0) {

				Character.Instance.MoveModifier = new Vector3(gameObject.rigidbody.velocity.x,
				                                                  0,
				                                                  gameObject.rigidbody.velocity.z);
			} else {
				// gameObject.rigidbody.velocity.y + 1
				// player falls through rising platform if he falls on at gravity speeds
				Character.Instance.MoveModifier = new Vector3(gameObject.rigidbody.velocity.x,
				                                                  gameObject.rigidbody.velocity.y + Mathf.Abs(Character.Instance.MoveVector.y),
				                                                  gameObject.rigidbody.velocity.z);
			}
		} else {
			Character.Instance.MoveModifier = Vector3.zero;
		}
	}
	
	void GoToTarget(Vector3 StartPos, Vector3 EndPos, string AnimationName) {
		// create curves for transformation path
		AnimationCurve xpath = AnimationCurve.Linear(0, StartPos.x, Speed, EndPos.x);
		AnimationCurve ypath = AnimationCurve.Linear(0, StartPos.y, Speed, EndPos.y);
		AnimationCurve zpath = AnimationCurve.Linear(0, StartPos.z, Speed, EndPos.z);
		AnimationClip clip = new AnimationClip();
		// add curves to clip so they are animated
		clip.SetCurve("", typeof(Transform), "localPosition.x", xpath);
		clip.SetCurve("", typeof(Transform), "localPosition.y", ypath);
		clip.SetCurve("", typeof(Transform), "localPosition.z", zpath);
		MoveAnim.AddClip(clip, AnimationName);
		MoveAnim.wrapMode = WrapMode.Once;
	}
}
