using UnityEngine;
using System.Collections;

public class PlayerMotor2D : MonoBehaviour {


	public LayerMask staticEnvLayerMask;

	public float envCheckDistance = 0.04f;
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


	public float groundSpeed = 8f;
	public float timeToGroundSpeed = 0.1f;
	public float groundStopDistance = 0.333f;

	public float airSpeed = 5f;
	public bool changeDirectionInAir = true;
	public float timeToAirSpeed = 0.2f;
	public float airStopDistance = 2f;


	public float fallSpeed = 16f;
	public float gravityMultiplier = 4;

	public float jumpHeight = 1.5f;
	public float extraJumpHeight = 1.5f;
	public int numOfAirJumps = 1;

	public enum MotorState{
		OnGround,
		Jumping,
		Falling
	}

	public enum CollidedSurface{
		None,
		Ground,
		LeftWall,
		RightWall
	}

	public MotorState motorState;
	public CollidedSurface collidingAgaisnt;

	public bool IsOnGround(){
		return motorState == MotorState.OnGround;
	}


	#region Private

	private LayerMask _collisionMask;

	private Rigidbody2D _rigidbody2D;
	private Vector2 _velocity;

	private Collider2D _collider2D;

	#endregion

	private void Awake(){
		_collider2D = GetComponent<Collider2D> ();
		_rigidbody2D = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () {
		_collisionMask = staticEnvLayerMask;
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	
	}

	private RaycastHit2D GetClosestHit(Vector2 origin, Vector3 direction, float distance, bool useBoxCast = true, bool checkWereTouching = false){
		if(useBoxCast){
			return Physics2D.BoxCast (origin, _collider2D.bounds.size, 0f, direction, distance, _collisionMask);
		}

		return Physics2D.Raycast (origin, direction, distance, _collisionMask);
	}


	private CollidedSurface CheckGround(float distance)
	{
		CollidedSurface surfaces = CollidedSurface.None;

		RaycastHit2D closestHit = GetClosestHit (_collider2D.bounds.center, Vector3.down, distance);

		if(closestHit.collider !=null){
			surfaces |= CollidedSurface.Ground;

			if(_collider2D.bounds.center.y - closestHit.centroid.y < minDistanceFromEnv){
				transform.position += (minDistanceFromEnv - (_collider2D.bounds.center.y - closestHit.centroid.y)) * Vector3.up;
			}
		}

		return surfaces;
	}


	private void ChangeState(MotorState newState){
		//no change
		if(motorState == newState)
		{
			return;
		}

		//set new state
		motorState = newState;
	}
}
