using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Actions))]
public class PlayerController : MonoBehaviour {

	private Actions playerActions;
	private float movement;
	private bool jump;
	private bool shoot;

	// Use this for initialization
	void Start () {
		playerActions = GetComponent<Actions>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!jump){
			jump = Input.GetButtonDown("Jump");
		}
		shoot = Input.GetButtonDown("Fire3");
		playerActions.Move(movement, jump, shoot);
		jump = false;
	}

	private void FixedUpdate()
	{
		movement = Input.GetAxis("Horizontal");		
	}
}
