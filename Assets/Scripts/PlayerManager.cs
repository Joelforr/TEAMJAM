using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public WorldManager worldManagerScript;
	public PlatformerMotor2D motorScript;
	public SpriteRenderer Background;

	Color lightWorld = new Color32(180, 130, 180, 255);
	Color darkWorld = new Color32(140, 40, 140, 255);

	//EDITABLE PHYSICS VARIABLES
	public float groundSmashDist =5f;

	public float cloakedGroundSpeed = 14f;
	public float cloakedTimeToGroundSpeed = 0.1f;
	public float cloakedGroundStopDistance = .333f;
	public float cloakedAirSpeed = 17f;
	public float cloakedAirStopDistance = 7f;
	public float cloakedFallSpeed = 10f;
	public float cloakedGravityMultiplier = 2.5f;
	public float cloakedJumpHeight = 8f;
	public float cloakedExtraJumpHeight = 4.5f;
	public int cloakedNumOfAirJumps = 1;

	public float uncloakedGroundSpeed = 30f;
	public float uncloakedTimeToGroundSpeed = 1f;
	public float uncloakedGroundStopDistance = 7.333f;
	public float uncloakedAirSpeed = 7f;
	public float uncloakedAirStopDistance = 2f;
	public float uncloakedFallSpeed = 20f;
	public float uncloakedGravityMultiplier = 2.5f;
	public float uncloakedJumpHeight = 5f;
	public float uncloakedExtraJumpHeight = 3.5f;
	public int uncloakedNumOfAirJumps = 0;

	public enum PlayerState{
		Cloaked,
		Uncloaked_Form1,
		Uncloaked_Form2,
		Uncloaked_Form3
	}

	public PlayerState duaeState;

	// Use this for initialization
	void Start () {
		
		duaeState = PlayerState.Cloaked;
		worldManagerScript = FindObjectOfType<WorldManager>();
		motorScript = GetComponent<PlatformerMotor2D> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		if(worldManagerScript.ShiftValidtyCheck() && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))){
			print("Times Pressed:");
			duaeState = ChangePlayerState (duaeState);

		}

		if(duaeState == PlayerState.Cloaked){
			motorScript.staticEnvLayerMask = ((1 << LayerMask.NameToLayer("StaticEnvironment")) | (1 << LayerMask.NameToLayer("LightWorldEnvironment")));
			motorScript.dynamicEnvLayerMask = 1 << LayerMask.NameToLayer ("DynamicEnvironment");
			motorScript.groundSpeed = cloakedGroundSpeed;
			motorScript.timeToGroundSpeed = cloakedTimeToGroundSpeed;
			motorScript.groundStopDistance = cloakedGroundStopDistance;
			motorScript.airSpeed = cloakedAirSpeed;
			motorScript.airStopDistance = cloakedAirStopDistance;
			motorScript.fallSpeed = cloakedFallSpeed;
			motorScript.gravityMultiplier = cloakedGravityMultiplier;
			motorScript.jumpHeight = cloakedJumpHeight;
			motorScript.extraJumpHeight = cloakedExtraJumpHeight;
			motorScript.numOfAirJumps = cloakedNumOfAirJumps;
			motorScript.enableDestruction = false;
			motorScript.enableObjectPushing = false;
			motorScript.enableWallSticks = false;
			motorScript.enableWallJumps = false;
			motorScript.enableWallSlides = false;
			motorScript.enableCornerGrabs = false;
			Background.GetComponent<SpriteRenderer>().color = lightWorld;
		}

		if(duaeState == PlayerState.Uncloaked_Form1 ){
			motorScript.staticEnvLayerMask = (1 << LayerMask.NameToLayer("StaticEnvironment") | 1 << LayerMask.NameToLayer("DarkWorldEnvironment"));
			motorScript.dynamicEnvLayerMask = 1 << LayerMask.NameToLayer("DynamicEnvironment");
			motorScript.groundSpeed = uncloakedGroundSpeed;
			motorScript.timeToGroundSpeed = uncloakedTimeToGroundSpeed;
			motorScript.groundStopDistance = uncloakedGroundStopDistance;
			motorScript.airSpeed = uncloakedAirSpeed;
			motorScript.airStopDistance = uncloakedAirStopDistance;
			motorScript.fallSpeed = uncloakedFallSpeed;
			motorScript.gravityMultiplier = uncloakedGravityMultiplier;
			motorScript.jumpHeight = uncloakedJumpHeight;
			motorScript.extraJumpHeight = uncloakedExtraJumpHeight;
			motorScript.numOfAirJumps = uncloakedNumOfAirJumps;
			motorScript.enableDestruction = true;
			motorScript.enableObjectPushing = true;
			motorScript.enableWallSticks = true;
			motorScript.enableWallJumps = true;
			motorScript.enableWallSlides = true;
			motorScript.enableCornerGrabs = true;
			motorScript.minDistanceToGroundSlam = groundSmashDist;
			Background.GetComponent<SpriteRenderer>().color = darkWorld;
		}

		if(duaeState == PlayerState.Uncloaked_Form2 ){
			motorScript.staticEnvLayerMask = (1 << LayerMask.NameToLayer("StaticEnvironment") | 1 << LayerMask.NameToLayer("DarkWorldEnvironment"));
			motorScript.dynamicEnvLayerMask = 1 << LayerMask.NameToLayer("DynamicEnvironment");
			motorScript.groundSpeed = uncloakedGroundSpeed;
			motorScript.timeToGroundSpeed = uncloakedTimeToGroundSpeed;
			motorScript.groundStopDistance = uncloakedGroundStopDistance;
			motorScript.airSpeed = uncloakedAirSpeed;
			motorScript.airStopDistance = uncloakedAirStopDistance;
			motorScript.fallSpeed = uncloakedFallSpeed;
			motorScript.gravityMultiplier = uncloakedGravityMultiplier;
			motorScript.jumpHeight = uncloakedJumpHeight;
			motorScript.extraJumpHeight = uncloakedExtraJumpHeight;
			motorScript.numOfAirJumps = uncloakedNumOfAirJumps;
			motorScript.enableDestruction = true;
			motorScript.enableObjectPushing = true;
			motorScript.enableWallSticks = true;
			motorScript.enableWallJumps = true;
			motorScript.enableWallSlides = true;
			motorScript.enableCornerGrabs = true;
			motorScript.minDistanceToGroundSlam = groundSmashDist;
			Background.GetComponent<SpriteRenderer>().color = darkWorld;
		}

		if(duaeState == PlayerState.Uncloaked_Form3 ){
			motorScript.staticEnvLayerMask = (1 << LayerMask.NameToLayer("StaticEnvironment") | 1 << LayerMask.NameToLayer("DarkWorldEnvironment"));
			motorScript.dynamicEnvLayerMask = 1 << LayerMask.NameToLayer("DynamicEnvironment");
			motorScript.groundSpeed = uncloakedGroundSpeed;
			motorScript.timeToGroundSpeed = uncloakedTimeToGroundSpeed;
			motorScript.groundStopDistance = uncloakedGroundStopDistance;
			motorScript.airSpeed = uncloakedAirSpeed;
			motorScript.airStopDistance = uncloakedAirStopDistance;
			motorScript.fallSpeed = uncloakedFallSpeed;
			motorScript.gravityMultiplier = uncloakedGravityMultiplier;
			motorScript.jumpHeight = uncloakedJumpHeight;
			motorScript.extraJumpHeight = uncloakedExtraJumpHeight;
			motorScript.numOfAirJumps = uncloakedNumOfAirJumps;
			motorScript.enableDestruction = true;
			motorScript.enableObjectPushing = true;
			motorScript.enableWallSticks = true;
			motorScript.enableWallJumps = true;
			motorScript.enableWallSlides = true;
			motorScript.enableCornerGrabs = true;
			motorScript.minDistanceToGroundSlam = groundSmashDist;
			Background.GetComponent<SpriteRenderer>().color = darkWorld;
		}
	}

	PlayerState ChangePlayerState(PlayerState currentState){
		print ("CurState" + currentState);
		if (currentState == PlayerState.Cloaked){
			//Dont actually know level values fix this when levels are organized in editor
			if(worldManagerScript.levelsCompletedList.Contains(3)){
				currentState = PlayerState.Uncloaked_Form3;
			}
			else if(worldManagerScript.levelsCompletedList.Contains(2)){
				currentState = PlayerState.Uncloaked_Form2;			
				}
			else /*if(worldManagerScript.levelsCompletedList.Contains(1))*/{
					currentState = PlayerState.Uncloaked_Form1;
				}
		}
		else if (currentState == PlayerState.Uncloaked_Form1 || 
			currentState == PlayerState.Uncloaked_Form2 || 
			currentState == PlayerState.Uncloaked_Form3 )
		currentState = PlayerState.Cloaked;

	return currentState;
	}

}
