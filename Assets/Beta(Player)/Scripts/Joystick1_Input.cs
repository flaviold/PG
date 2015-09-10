using UnityEngine;
using System.Collections;

[RequireComponent(typeof (CharacterActions))]
public class Joystick1_Input : MonoBehaviour {

	private CharacterActions playerActions;
	
	void Awake(){
		playerActions = GetComponent<CharacterActions>();
	}
	
	void Update(){
		if (Input.GetButtonDown("Fire1")) {
			playerActions.Jump();
		}
		if (Input.GetButtonDown("Fire3")) {
			playerActions.Shoot();
		}
		if(Input.GetButtonDown("Fire5")) {
			playerActions.Defend();
		}
		playerActions.Crouching(Input.GetButton("Fire6"));
	}

	private void FixedUpdate(){
		playerActions.Move(Input.GetAxis("Horizontal"));		
	}
}
