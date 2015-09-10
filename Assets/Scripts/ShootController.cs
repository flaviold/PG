using UnityEngine;
using System.Collections;

public class ShootController : MonoBehaviour {

	public float speed;
	
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Shield"){
			Vector3 tempVec = GetComponent<Rigidbody>().velocity;
			tempVec.x *= -1;
			GetComponent<Rigidbody>().velocity = tempVec;
			return;
		}
		if (other.tag == "Player"){
			GameManager.Kill(other.name);
		}
		Destroy(gameObject);
	}
	
	public void Shoot(float rotation) {
		Vector3 direction;
		if (rotation < 0) {
			direction = new Vector3(-1, 0, 0);
		} else {
			direction = new Vector3(1, 0, 0);
		}
		GetComponent<Rigidbody>().velocity = direction * speed * Time.deltaTime;
	}
}
