#pragma strict

var turnRate:float = 15;
var speed:float = 5;
var jumpPower:float = 8;
var speedDampTime:float = 0.1;

private var _anim:Animator;
private var _rigidbody:Rigidbody;
private var _grounded:boolean;
private var _jump:boolean;
private var _move:float;
private var _lastYVelocity:float;


function Awake () {
	_anim = GetComponent.<Animator>();
	_rigidbody = GetComponent.<Rigidbody>();
	_grounded = true;
	_lastYVelocity = 0;
}

function Update() {
	_jump = Input.GetButtonDown("Fire1");
	
	Jump(_jump);
	CheckGround();
}

function FixedUpdate () {
	if (gameObject.name == "1"){
		_move = Input.GetAxis("Horizontal");
	}
	
	Move(_move);
}

function Move(movement:float) {
	if (movement != 0) {
		Rotate(movement);
		_anim.SetFloat("Speed", speed, speedDampTime, Time.deltaTime);
	} else {
		_anim.SetFloat("Speed", 0);
	}
}

function Jump(jump:boolean) {
	if (GameManager.GetJoystickNumber() == 	int.Parse(gameObject.name)) {
		if (_grounded && _jump) {
			_rigidbody.velocity.y = jumpPower;
		}
	}
}

function Rotate(movement:float) {
	var direction = Vector3(movement, 0, 0);
	var rotation = Quaternion.LookRotation(direction);
	
	_rigidbody.MoveRotation(rotation);
}

function CheckGround() {
	if ((_lastYVelocity < -0.1 && _rigidbody.velocity.y > -0.1 && _rigidbody.velocity.y < 0.1) || (_grounded && _rigidbody.velocity.y > -0.1 && _rigidbody.velocity.y < 0.1)) {
		_grounded = true;
	} else {
		_grounded = false;
	}
	
	_lastYVelocity = _rigidbody.velocity.y;
}