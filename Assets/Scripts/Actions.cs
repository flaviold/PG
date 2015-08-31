using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animator))]
public class Actions : MonoBehaviour {

	public float speed = 8f;
	public float speedDampTime = 0.1f;
	public float jumpPower = 5f;

	public float fireRate;
	public GameObject shot;
	public Transform shotSpawn;

	private bool shooting = false;
	private float nxtFire = 1;
	private float fireAnimation = 1;

	const int countOfDamageAnimations = 1;
	int lastDamageAnimation = -1;

	private bool isGrounded = true;

	private Animator animator;
	private Rigidbody rigidbody;

	void Awake () {
		animator = GetComponent<Animator>();
		rigidbody = GetComponent<Rigidbody>();
		CheckGroundStatus();
	}
	void Update () {
		CheckGroundStatus();
	}

	public void Move(float movement, bool jump, bool shoot) {
		Jump(jump);
		Shoot(shoot);
		if (movement != 0) {
			Rotate(movement);
			animator.SetFloat("Speed", Mathf.Abs(movement) * speed);
			rigidbody.velocity = new Vector3(movement * speed, rigidbody.velocity.y, rigidbody.velocity.z);
		} else {
			rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, rigidbody.velocity.z);
			animator.SetFloat("Speed", 0);
		}
	}

	public void Rotate(float movement) {
		Vector3 direction = new Vector3(movement, 0, 0);
		Quaternion rotation = Quaternion.LookRotation(direction);
		
		rigidbody.MoveRotation(rotation);
	}

	public void Shoot (bool shoot) {
		if (shooting && fireAnimation <= 0.1){
			shooting = false;
			float rotation = gameObject.transform.rotation.y;
			GameObject shotClone = (GameObject)Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			shotClone.GetComponent<ShootController>().Shoot(rotation);
			fireAnimation = 1;
		} else {
			if (shoot && Time.time > nxtFire) {
				nxtFire = Time.time + fireRate;
				shooting = true;
			} else if (shooting){
				fireAnimation = (nxtFire - Time.time)/fireRate;
			}
		}
		//animator.SetFloat("Shooting", Mathf.Abs(fireAnimation));
	}

	public void Death () {
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Death"))
			animator.Play("Idle", 0);
		else
			animator.SetTrigger ("Death");
	}

	public void Damage () {
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Death")) return;
		int id = Random.Range(0, countOfDamageAnimations);
		if (countOfDamageAnimations > 1)
			while (id == lastDamageAnimation)
				id = Random.Range(0, countOfDamageAnimations);
		lastDamageAnimation = id;
		animator.SetInteger ("DamageID", id);
		animator.SetTrigger ("Damage");
	}

	public void Jump(bool jump) {
		if (isGrounded && jump) {
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpPower, rigidbody.velocity.z);
			isGrounded = false;
			animator.applyRootMotion = false;
			animator.SetTrigger ("Jump");
		}
		if (!isGrounded){
			animator.SetFloat("Jump", rigidbody.velocity.y);
		}

	}

	public void Aiming () {
		animator.SetBool ("Squat", false);
		animator.SetFloat ("Speed", 0f);
		animator.SetBool("Aiming", true);
	}

	public void Sitting () {
		animator.SetBool ("Squat", !animator.GetBool("Squat"));
		animator.SetBool("Aiming", false);
	}

	void CheckGroundStatus()
	{
		RaycastHit hitInfo;
		// 0.1f is a small offset to start the ray from inside the character
		// it is also good to note that the transform position in the sample assets is at the base of the character
		if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, 0.1f))
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
