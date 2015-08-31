using UnityEngine;
using System.Collections;

public class ShotController : MonoBehaviour {

	public float speed;

	void OnTriggerEnter (Collider other) {
		if(other.tag == "Player"){
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
