#pragma strict

import UnityEngine.UI;

static var placar1:int = 0;
static var placar2:int = 0;
static var round:int = 1;

var p1:Text;
var p2:Text;
var roundSign:Text;

var playerModel:GameObject;
var spawnPoint:Transform[];

private static var _player1:GameObject;
private static var _player2:GameObject;


function Start() {
	_player1 = Instantiate(playerModel, spawnPoint[0].position, spawnPoint[0].rotation) as GameObject;
	_player1.name = "1";
	_player2 = Instantiate(playerModel, spawnPoint[1].position, spawnPoint[1].rotation) as GameObject;
	_player2.name = "2";
}

function Update () {
	HUDUpdate();
	if (!_player2 && !_player1 && GameManager.round < 5){
		_player1 = Instantiate(playerModel, spawnPoint[0].position, spawnPoint[0].rotation) as GameObject;
		_player1.name = "1";
		_player2 = Instantiate(playerModel, spawnPoint[1].position, spawnPoint[1].rotation) as GameObject;
		_player2.name = "2";
	}
}

static function GetJoystickNumber() : int {
    var joyNum : int = 1;
    var buttonNum : int = 0;
    var keyCode : int = 350;
   
    for(var i : int = 0; i < 60; i++) {
       
        if(Input.GetKeyDown(keyCode+i)) return joyNum;
       
        buttonNum++;
       
        if(buttonNum == 20) {
            buttonNum = 0;
            joyNum++;
        }
    }
    
    return 0;
}

static function Kill(playerName:String) {
	if (playerName == "1") {
		Destroy(_player1);
		placar2++;
	} else {
		Destroy(_player2);
		placar1++;
	}
	
	if (round == 5) {
		GameOver();
	} else {
		round++;
		GameOver();
	}
}

static function Restart() {
	Destroy(_player1);
	Destroy(_player2);
	//_player1 = Instantiate(playerModel, spawnPoint[0].position, spawnPoint[0].rotation) as GameObject;
	//_player1.name = "1";
	//_player2 = Instantiate(playerModel, spawnPoint[1].position, spawnPoint[1].rotation) as GameObject;
	//_player2.name = "2";
}

static function GameOver() {
	Destroy(_player1);
	Destroy(_player2);
}

function HUDUpdate() {
	p1.text = "Player 1: " + GameManager.placar1;
 	p2.text = "Player 2: " + GameManager.placar2;
 	if (round == 5) {
 		if (placar1 > placar2){
 			roundSign.text = "Player 1 Won";
 		} else {
 			roundSign.text = "Player 2 Won";
 		}
	} else {
		roundSign.text = "Round " + GameManager.round;
	}
}