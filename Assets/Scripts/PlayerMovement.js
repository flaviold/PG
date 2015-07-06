#pragma strict

var turnRate:float = 15;
var speed:float = 5;
var speedDampTime:float = 0.1;

private var _anim:Animator;
private var _rigidbody:Rigidbody;
private var _grounded:boolean;

function Awake () {
	_anim = GetComponent.<Animator>();
	_rigidbody = GetComponent.<Rigidbody>();
	_grounded = true;
}

function FixedUpdate () {
	var move = Input.GetAxis("Horizontal");
	var	jump = Input.GetButtonDown("Jump");
	
	Move(move, jump);
}

function Move(movement:float, jump:boolean) {
	if (movement != 0) {
		Rotate(movement);
		_anim.SetFloat("Speed", speed, speedDampTime, Time.deltaTime);
	} else {
		_anim.SetFloat("Speed", 0);
	}
	
	if (_grounded) {
		_anim.SetBool("Jumping", jump);
	}
	
}

function Rotate(movement:float) {
	var direction = Vector3(movement, 0, 0);
	var rotation = Quaternion.LookRotation(direction);
	
	_rigidbody.MoveRotation(rotation);
}