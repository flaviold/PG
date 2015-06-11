﻿#pragma strict

//Requirements
@script RequireComponent(typeof(PlayerPhysics))

//Player Handling
var speed:float = 8;
var acceleration:float = 12;

private var _currentSpeed:float;
private var _targetSpeed:float;
private var _movementVector:Vector2;

private var _playerPhysics:PlayerPhysics;

function Start() {
	_playerPhysics = GetComponent.<PlayerPhysics>();
}

function Update() {
	_targetSpeed = Input.GetAxisRaw("Horizontal") * speed;
	IncrementSpeed();
	
	_movementVector = Vector2(_currentSpeed, 0);
	_playerPhysics.Move(_movementVector * Time.deltaTime);
}

private function IncrementSpeed() {
	var dir:float;
	var cSpeed:float = _currentSpeed;
	
	if (_targetSpeed == cSpeed) return;
	
	dir = Mathf.Sign(_targetSpeed - cSpeed);
	cSpeed += acceleration * Time.deltaTime * dir;
	
	_currentSpeed = (dir == Mathf.Sign(_targetSpeed - cSpeed)) ? cSpeed : _targetSpeed;
}

