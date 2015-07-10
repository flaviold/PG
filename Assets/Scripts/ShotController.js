#pragma strict

var speed : float;

function OnTriggerEnter (other: Collider) {
	if(other.tag == "Player"){
		GameManager.Kill(other.name);
	}
	Destroy(gameObject);
}

function Shot(rotation:float) {
	var direction:Vector3;
	if (rotation < 0) {
		direction = Vector3(-1, 0, 0);
	} else {
		direction = Vector3(1, 0, 0);
	}
	GetComponent.<Rigidbody>().velocity = direction * speed * Time.deltaTime;
}