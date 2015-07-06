#pragma strict

var gravity:float = 1;

private var _rb: Rigidbody;
private var _grounded = true;

function Start() {
	_rb = GetComponent.<Rigidbody>();
	
	Physics.gravity = Vector3(0, -gravity, 0);
}

function Move(movementVector:Vector2) {
	_rb.velocity.x = movementVector.x;
	//transform.Translate(movementVector);
}

function Jump(jumpPower:float) {
	_rb.velocity.y = jumpPower;
}

function isGrounded() {
	return _grounded;
}