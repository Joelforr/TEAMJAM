using UnityEngine;
using System.Collections;

public class PlyrController2D : MonoBehaviour {

	private PlayerMotor2D _motor;
/*
	// Use this for initialization
	void Start () {
	
		_motor = GetComponent<PlayerMotor2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		//Jump?
		if(Input.GetButtonDown("Jump")){

			_motor.Jump();
		}

		_motor.jumpingHeld = Input.GetButton ("Jump");


		//Horizontal Movement
		if(Mathf.Abs(Input.GetAxis("Horizontal")) > .5f){
			_motor.normalizedXMovement = Input.GetAxis ("Horizontal");
		}
		else{
			_motor.normalizedXMovement = 0;
		}
	}*/
}
