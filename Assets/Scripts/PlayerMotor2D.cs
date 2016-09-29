using UnityEngine;
using System.Collections;

public class PlayerMotor2D : MonoBehaviour {

	/// <summary>
	/// The static environment check mask. This should only be environment that doesn't move.
	/// </summary>
	public LayerMask staticEnvLayerMask;

	/// <summary>
	/// How far out the motor will check for the environment mask. This value can be tweaked if jump checks are not firing when
	/// wanted.
	/// </summary>
	public float envCheckDistance = 0.04f;

	/// <summary>
	/// The distance the motor will separate itself from environment. This is useful to prevent the motor from catching on edges.
	/// </summary>
	public float minDistanceFromEnv = 0.02f;

	/// <summary>
	/// The number of iterations the motor is allowed to make during the fixed update. Lower number will be more performant
	/// at a cost of losing some movement when collisions occur.
	/// </summary>
	public int numOfIterations = 2;

	/// <summary>
	/// The layer that contains moving platforms. If there are no moving platforms then make sure this has no layers (value of 0).
	/// Optimizations are made in the motor if it isn't expecting any moving platforms.
	/// </summary>
	public LayerMask movingPlatformLayerMask;

	/// <summary>
	/// When checking for moving platforms that may have moved into the motor the corners are automatically casted on. This
	/// variable impacts how many more casts are made to each side. If the smallest size environment is the same size or bigger
	/// than the motor (width and height) then this can be 0. If it's at least half size then this can be 1. Increasing this
	/// number allows separation from smaller platform pieces but at a performance cost.
	/// </summary>
	public int additionalRaycastsPerSide = 1;

	/// <summary>
	/// The maximum speed the motor will move on the ground, only effects horizontal speed.
	/// </summary>
	public float groundSpeed = 8f;

	/// <summary>
	/// How much time does it take for the motor to get from zero speed to max speed. This value
	/// is used to calculate the acceleration.
	/// </summary>
	public float timeToGroundSpeed = 0.1f;

	/// <summary>
	/// The distance the motor will slide to a stop from full speed while on the ground.
	/// </summary>
	public float groundStopDistance = 0.333f;

	/// <summary>
	/// The maximum horizontal speed of the motor in the air.
	/// </summary>
	public float airSpeed = 5f;

	/// <summary>
	/// If true, then the player can change x direction while jumping. If false, then
	/// the x velocity when leaving the ground will be held while in the air
	/// </summary>
	public bool changeDirectionInAir = true;

	/// <summary>
	/// The time it takes to move from zero horizontal speed to the maximum speed. This value is
	/// used to calculate the acceleration.
	/// </summary>
	public float timeToAirSpeed = 0.2f;

	/// <summary>
	/// The distance the motor will 'slide' to a stop while in the air. Only effects horizontal
	/// movement.
	/// </summary>
	public float airStopDistance = 2f;

	/// <summary>
	/// The maximum speed that the motor will fall. Only effects vertical speed when falling.
	/// </summary>
	public float fallSpeed = 16f;

	/// <summary>
	/// Gravity multiplier to the Physics2D.gravity setting. Works like RigidBody2D's gravityMultiplier.
	/// </summary>
	public float gravityMultiplier = 4;

	/// <summary>
	/// The maximum speed that the motor will fall during 'fast fall'.
	/// </summary>
	public float fastFallSpeed = 32f;

	/// <summary>
	/// If the motor is in 'fast fall' then the gravityMultiplier is multiplied by the value. Higher number means
	/// faster acceleration while falling.
	/// </summary>
	public float fastFallGravityMultiplier = 8f;

	/// <summary>
	/// The height the motor will jump when a jump command is issued.
	/// </summary>
	public float jumpHeight = 1.5f;

	/// <summary>
	/// The extra height the motor will jump if jump is 'held' down.
	/// </summary>
	public float extraJumpHeight = 1.5f;

	/// <summary>
	/// Number of air jumps allowed.
	/// </summary>
	public int numOfAirJumps = 1;

	/// <summary>
	/// The amount of time once the motor has left an environment that a jump will be allowed.
	/// </summary>
	public float jumpWindowWhenFalling = 0.2f;

	/// <summary>
	/// The grace period once the motor is told to jump where it will jump.
	/// </summary>
	public float jumpWindowWhenActivated = 0.2f;

	/// <summary>
	/// Should the motor check for any slopes? Set this to false if there are no slopes, the motor will be more efficient.
	/// </summary>
	public bool enableSlopes = true;

	/// <summary>
	/// The angle of slope the motor is allowed to walk on. It could be a good idea to keep this slightly above the minimum.
	/// </summary>
	public float angleAllowedForMoving = 50;

	/// <summary>
	/// The speed necessary to try running up a slope that's too steep. If speed is less than the minimum then the motor's
	/// velocity is zeroed out and the motor can't try to run up the slope.
	/// </summary>
	public float minimumSpeedToMoveUpSlipperySlope = 7.5f;

	/// <summary>
	/// Should the speed of the motor change depending of the angle of the slope. This only impacts walking on slopes, not
	/// while sliding.
	/// </summary>
	public bool changeSpeedOnSlopes = true;

	/// <summary>
	/// If the motor changes speed on slopes then this acts as a multiplier. Lower values will mean bigger slow downs. A value
	/// of 1 means that it's only based off of the angle of the slope.
	/// </summary>

	[Range(0f, 1f)]
	public float speedMultiplierOnSlope = 0.75f;

	/// <summary>
	/// Should the motor stick to the ground when walking down onto a slope or up over a slope? Otherwise the motor may fall onto
	/// the slope or have a slight hop when moving up over a slope.
	/// </summary>
	public bool stickOnGround = true;

	/// <summary>
	/// If stickOnGround is true then the motor will search down for the ground to place itself on. This is how far it is willing
	/// to check. This needs to be high enough to account for the distance placed by the motor speed but should be smaller than
	/// the difference between environment heights. Play around until a nice value is found.
	/// </summary>
	public float distanceToCheckToStick = 0.4f;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
