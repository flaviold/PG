#pragma strict

var shot:GameObject;
var shotSpawn:Transform;
var fireRate:float;

private var _nxtFire:float = 0;

function Update () {
	if (GameManager.GetJoystickNumber() == 	int.Parse(gameObject.name)) {
		if (Input.GetButtonDown("Fire3") && Time.time > _nxtFire) {
			_nxtFire = Time.time + fireRate;
			var rotation = gameObject.transform.rotation.y;
			var shotClone = Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject;
			shotClone.GetComponent.<ShotController>().Shot(rotation);
		}
	}
}