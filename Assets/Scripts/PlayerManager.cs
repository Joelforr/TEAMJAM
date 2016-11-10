using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public PlatformerMotor2D motorScript;
	public SpriteRenderer Background;

	Color lightWorld = new Color32(180, 130, 180, 90);
		Color darkWorld = new Color32(140, 40, 140, 90);

	public enum PlayerState{
		Cloaked,
		Uncloaked
	}

	public PlayerState duaeState;

	// Use this for initialization
	void Start () {
		
		duaeState = PlayerState.Cloaked;
		motorScript = GetComponent<PlatformerMotor2D> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)){
			print("Times Pressed:");
			duaeState = ChangePlayerState (duaeState);
		}

		if(duaeState == PlayerState.Cloaked){
			motorScript.staticEnvLayerMask = ((1 << LayerMask.NameToLayer("StaticEnvironment")) | (1 << LayerMask.NameToLayer("LightWorldEnvironment")));
			motorScript.groundSpeed = 14f;
			motorScript.timeToGroundSpeed = 0.1f;
			motorScript.groundStopDistance = .333f;
			motorScript.airSpeed = 17f;
			motorScript.airStopDistance = 7f;
			motorScript.fallSpeed = 10f;
			motorScript.gravityMultiplier = 2.5f;
			motorScript.jumpHeight = 8f;
			motorScript.extraJumpHeight = 4.5f;
			motorScript.numOfAirJumps = 1;
			Background.GetComponent<SpriteRenderer>().color = lightWorld;
		}

		if(duaeState == PlayerState.Uncloaked ){
			motorScript.staticEnvLayerMask = (1 << LayerMask.NameToLayer("StaticEnvironment") | 1 << LayerMask.NameToLayer("DarkWorldEnvironment"));
			motorScript.groundSpeed = 30f;
			motorScript.timeToGroundSpeed = 1f;
			motorScript.groundStopDistance = 7.333f;
			motorScript.airSpeed = 7f;
			motorScript.airStopDistance = 2f;
			motorScript.fallSpeed = 20f;
			motorScript.gravityMultiplier = 2.5f;
			motorScript.jumpHeight = 5f;
			motorScript.extraJumpHeight = 3.5f;
			motorScript.numOfAirJumps = 0;
			Background.GetComponent<SpriteRenderer>().color = darkWorld;
		}
	}

	PlayerState ChangePlayerState(PlayerState currentState){
		print ("CurState" + currentState);
	if (currentState == PlayerState.Cloaked)
		currentState = PlayerState.Uncloaked;
	else if (currentState == PlayerState.Uncloaked)
		currentState = PlayerState.Cloaked;

	return currentState;
	}

}
