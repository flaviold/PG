using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animator))]
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]
public class CharacterActions : MonoBehaviour {

	public float speed = 8f;
	public float crouchSpeed = 5f;
	public float speedDampTime = 0.1f;
	public float jumpPower = 5f;

	public float shieldRate;
	public float fireRate;
	public GameObject shield;
	public GameObject shot;
	public Transform shotSpawn;

	private float nxtShield = 0;
	private float nxtFire = 0;
	private float fireAnimation = 1;
	
	const int countOfDamageAnimations = 1;
	int lastDamageAnimation = -1;
	
	private bool isGrounded = true;
	private bool isCrouched = false;
	
	private Animator animator;
	private Rigidbody rigidbody;
	private CapsuleCollider capsCollider;
	
	void Awake () {
		animator = GetComponent<Animator>();
		rigidbody = GetComponent<Rigidbody>();
		capsCollider = GetComponent<CapsuleCollider>();
		CheckGroundStatus();
	}

	void Update () {
		CheckGroundStatus();
	}

	#region Character Actions
	public void Move(float movement){
		float relMov = isCrouched ? crouchSpeed : speed;
		if (movement != 0) {
			Rotate(movement);
			animator.SetFloat("Speed", Mathf.Abs(movement) * relMov);
			rigidbody.velocity = new Vector3(movement * relMov, rigidbody.velocity.y, rigidbody.velocity.z);
		} else {
			rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, rigidbody.velocity.z);
			animator.SetFloat("Speed", 0);
		}
	}

	public void Jump(){
		if (isGrounded) {
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpPower, rigidbody.velocity.z);
			isGrounded = false;
			animator.SetBool("Grounded", isGrounded);
		}
	}

	public void Crouching(bool crouch){
		float height = crouch ? 1.32f : 1.81f;
		float y = (isGrounded && crouch) ? 0.68f : 0.91f;
		Vector3 tempCenter = capsCollider.center;
		tempCenter.y = y;
		isCrouched = crouch;
		animator.SetBool("Crouched", isCrouched);
		capsCollider.height = height;
		capsCollider.center = tempCenter;
	}

	public void Shoot() {
		if (Time.time > nxtFire) {
			nxtFire = Time.time + fireRate;
			float rotation = gameObject.transform.rotation.y;
			GameObject shotClone = (GameObject)Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			shotClone.GetComponent<ShootController>().Shoot(rotation);
			animator.SetTrigger("Shoot");
		}
	}

	public void Defend(){
		if (Time.time > nxtShield) {
			nxtShield = Time.time + shieldRate;
			shield.GetComponent<Animator>().SetTrigger("Shield");
		}
	}
	#endregion

	private void Rotate(float movement) {
		Vector3 direction = new Vector3(movement, 0, 0);
		Quaternion rotation = Quaternion.LookRotation(direction);
		
		rigidbody.MoveRotation(rotation);
	}

	private void CheckGroundStatus()
	{
		float deadZone = 0.1f;
		RaycastHit hitInfo;
		// 0.1f is a small offset to start the ray from inside the character
		// it is also good to note that the transform position in the sample assets is at the base of the character
		if (Mathf.Abs(rigidbody.velocity.y) <= deadZone)
		{
			isGrounded = true;
			animator.SetBool("Grounded", isGrounded);
		}
		else
		{
			isGrounded = false;
			animator.SetBool("Grounded", isGrounded);
		}
	}
}
