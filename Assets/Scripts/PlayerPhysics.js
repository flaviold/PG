#pragma strict



function Start() {

}

//TODO: Melhorar colisao lateral
function Move(movementVector:Vector2) {
	
	transform.Translate(movementVector);
}

