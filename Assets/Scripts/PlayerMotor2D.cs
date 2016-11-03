using System;
using UnityEngine;
using System.Collections;

public class PlayerMotor2D : MonoBehaviour {
	
	/*
	public LayerMask staticEnvLayerMask;

	public float envCheckDistance = 0.04f;
	public float minDistanceFromEnv = 0.02f;

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

	[Flags]
	public enum CollidedSurface{
		None,
		Ground,
		LeftWall,
		RightWall,
		Cieling
	}

	public MotorState motorState;
	public CollidedSurface collidingAgaisnt;

	public float normalizedXMovement;
	public float normalizedYMovement;

	public Vector2 velocity{
		get{
			return _velocity;
		}

		set{
			_velocity = value;
		}
	}

	public bool facingLeft;

	public bool jumpingHeld{
		get{
			return _jumping.held;
		}

		set{
			if(!value){
				_jumping.held = false;
			}
		}
	}

	public void Jump(){
		_jumping.pressed = true;
		_jumping.height = jumpHeight;

		_jumping.held = true;
	}

	public void EndJump(){
		if(IsJumping()){
			_jumping.pressed = false;
			_jumping.numAirJumps = 0;
			ChangeState(MotorState.Falling);
		}
	}

	public bool IsJumping(){
		return motorState == MotorState.Jumping;
	}

	public bool IsFalling(){
		return motorState == MotorState.Falling;
	}

	public bool IsOnGround(){
		return motorState == MotorState.OnGround;
	}

	public bool IsInAir(){
		return IsJumping () || IsFalling ();
	}

	public bool IsGrounded(){
		return (HasFlag (CollidedSurface.Ground) && !IsJumping ());
	}
	

	#region Private

	private LayerMask _collisionMask;

	private float _timeScale = 1f;
	private Vector3 _toTransform;
	private float _currentDeltaTime;
	private Bounds _prevColliderBounds;
	private Rigidbody2D _rigidbody2D;
	private Collider2D _collider2D;

	private Vector2 _velocity;

	private class JumpState{
		public bool pressed;
		public bool held;
		public int numAirJumps;

		public float height;
	}

	private JumpState _jumping = new JumpState();

	private bool _ignoreGravity;

	private const float NEAR_ZERO = 0.0001f;

	#endregion

	private void Awake(){
		_collider2D = GetComponent<Collider2D> ();
		_rigidbody2D = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () {
		_collisionMask = staticEnvLayerMask;
	
	}
	private void UpdateSurroundings (bool forceCheck){
		if(forceCheck){
			collidingAgaisnt = CheckSurroundings (true);
		}
		else{

			collidingAgaisnt = CheckSurroundings(false);
		}
	}

	private void UpdateState(bool forceSurrondingsCheck){

		UpdateSurroundings(forceSurrondingsCheck);
	}

	// Update is called once per frame
	void FixedUpdate () {
		//this is for cooldowns
		//UpdateTimers();
		_collisionMask = staticEnvLayerMask;

		float time = Time.fixedDeltaTime;
	
	}

	private float UpdateMotor(float deltaTime){
		_currentDeltaTime = deltaTime;

		if(_prevColliderBounds != _collider2D.bounds){
			UpdateState (true);
		}

	}

	private bool HasFlag(CollidedSurface cs){
		return(collidingAgaisnt & cs) != CollidedSurface.None;
	}

	private void HandleFalling(){

		if (_ignoreGravity)
		{
			return;
		}

		if(IsInAir() && !_ignoreGravity){
			if(_velocity.y == -fallSpeed)
			{
				return;
			}

			if(_velocity.y > -fallSpeed){
				_velocity.y = Accelerate(_velocity.y, gravityMultiplier * Physics2D.gravity.y, -fallSpeed);
			}
			else{
				_velocity.y = Decelerate (_velocity.y, Math.Abs (gravityMultiplier * Physics.gravity.y), -fallSpeed);	
			}

			if(_velocity.y <= 0){
				ChangeState (MotorState.Falling);
			}
		}
	}
	private void ApplyMovement(){
		float speed;
		float maxSpeed;

		if(IsGrounded()){
			Vector2 moveDir = GetMovementDir (normalizedXMovement);

			GetSpeedAndMaxSpeedOnGround (out speed, out maxSpeed);

			if(timeToGroundSpeed > 0){ //if there is a time to max ground speed

				if(speed > 0 && 
					normalizedXMovement > 0 && 
					speed > normalizedXMovement * maxSpeed || //if were moving right faster than normalizedXmovement * maxspeed 
					speed < 0 && 
					normalizedXMovement < 0 &&
					speed < normalizedXMovement * maxSpeed || // if were moving left faster than normXMov * MaxSpeed 
					speed < 0 &&
					normalizedXMovement > 0 ||  // if we are moving left then try to go right 
					speed > 0 && 
					normalizedXMovement < 0)  //if we are moving right then try to go left
				{  
					float deceleration = (maxSpeed * maxSpeed) / (2 * groundStopDistance);

					speed = Decelerate (speed, deceleration, normalizedXMovement * maxSpeed);
				} 
				else
				{
					float acceleration = normalizedXMovement * (maxSpeed / timeToGroundSpeed);

					speed = Accelerate (speed, acceleration, normalizedXMovement * maxSpeed);
				}
			}
			else //if there is no time to ground speed
			{
				speed = normalizedXMovement * maxSpeed;
			}

			_velocity = GetMovementDir (speed) * Mathf.Abs (speed);
		}
		else if(changeDirectionInAir) // if we are in air and were allowed to change directions mid air
		{
			if(timeToAirSpeed > 0) // if theres a time to max air speed
			{
				if(_velocity.x > 0 &&
					normalizedXMovement > 0 &&
					_velocity.x > normalizedXMovement * airSpeed ||
					_velocity.x < 0 &&
					normalizedXMovement < 0 &&
					_velocity.x < normalizedXMovement * airSpeed)
				{

					speed = Decelerate (_velocity.x, (airSpeed * airSpeed) / (2 * airStopDistance), normalizedXMovement * airSpeed);
				}
				else
				{
					speed = Accelerate (_velocity.x, normalizedXMovement * (airSpeed / timeToAirSpeed), normalizedXMovement * airSpeed);
				}
			}
			else //if there is no time to air speed make speed = maxAirspeed
			{
				speed = normalizedXMovement * airSpeed;
			}

			_velocity.x = speed;
			
		}
		else if(_velocity.x !=0)
		{
			//START WORKING AGAIN FROM HEERE
			
		}
	}

	private void MovePosition(Vector3 newPos){
		if(newPos == _collider2D.bounds.center){
			return;
		}

		Vector3 toNewPos = newPos - _collider2D.bounds.center;
		float distance = toNewPos.magnitude;

		RaycastHit2D hit = GetClosestHit (_collider2D.bounds.center, toNewPos / distance, distance);

		if(hit.collider != null){
			transform.position = _toTransform + (Vector3)hit.centroid + (Vector3)hit.normal * minDistanceFromEnv;
		}else{
			transform.position = _toTransform + newPos;
		}
	}

	private Vector3 GetMovementDir(float speed){
		Vector3 moveDir = Vector3.zero;

		float multiplier = Mathf.Sign(speed);

		if(speed == 0){
			multiplier = Mathf.Sign (normalizedXMovement);
			speed = normalizedXMovement;
		}

		return Mathf.Sign (speed) * Vector3.right;

	}


	private void GetSpeedAndMaxSpeedOnGround(out float speed, out float maxSpeed){
		Vector3 moveDir = GetMovementDir(_velocity.x);

		//for non-slopes
		speed =_velocity.x;
		maxSpeed = groundSpeed;

	}

	private float Accelerate(float speed, float acceleration, float limit){
		speed += acceleration * Time.deltaTime;

		if(acceleration > 0)
		{
			if(speed > limit)
			{
				speed = limit;
			}
		}
		else{
			if( speed < limit){
				speed = limit;
			}
		}

		return speed;
	}

	private float Decelerate(float speed, float deceleration, float limit){
		if(speed < 0){
			speed += deceleration * Time.deltaTime;

			if(speed > limit)
			{
				speed = limit;
			}
		}
		else if(speed > 0){
			speed -= deceleration * Time.deltaTime;

			if(speed < limit)
			{
				speed = limit;
			}
		}

		return speed;
	}

	private void SetFacing(){
		if(normalizedXMovement < 0){

			facingLeft = true;
		}
		else if(normalizedXMovement > 0){
			
			facingLeft = false;
		}
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

	private CollidedSurface CheckSurroundings(bool forceCheck){
		CollidedSurface surfaces = CollidedSurface.None;

		Vector2 vecToCheck = _velocity;

		if(!forceCheck)
		{
			if(vecToCheck == Vector2.zero)
			{
				vecToCheck = Vector3.right * normalizedXMovement;
			}
		}

		RaycastHit2D closestHit;

		//LeftCheck
		if(forceCheck || -vecToCheck.x >= -NEAR_ZERO){
			closestHit = GetClosestHit (_collider2D.bounds.center, Vector3.left, envCheckDistance);

			if(closestHit.collider != null){
				surfaces |= CollidedSurface.LeftWall;

				if(_collider2D.bounds.center.x - closestHit.centroid.x < minDistanceFromEnv){
					transform.position += (minDistanceFromEnv - (_collider2D.bounds.center.x - closestHit.centroid.x)) * Vector3.right;
				}
			}
		}

		//CielingCheck
		if(forceCheck || vecToCheck.y >= -NEAR_ZERO){
			closestHit = GetClosestHit (_collider2D.bounds.center, Vector3.up, envCheckDistance);

			if(closestHit.collider != null){
				surfaces |= CollidedSurface.Cieling;

				if(closestHit.centroid.y - _collider2D.bounds.center.y < minDistanceFromEnv){
					transform.position += (minDistanceFromEnv - (closestHit.centroid.y - _collider2D.bounds.center.y)) * Vector3.down;
				}
			}
		}

		//RightCheck
		if(forceCheck || vecToCheck.x >= -NEAR_ZERO){
			closestHit = GetClosestHit (_collider2D.bounds.center, Vector3.right, envCheckDistance);

			if(closestHit.collider != null){
				surfaces |= CollidedSurface.RightWall;

				if(closestHit.centroid.x - _collider2D.bounds.center.x < minDistanceFromEnv){
					transform.position += (minDistanceFromEnv - (closestHit.centroid.x - _collider2D.bounds.center.x)) * Vector3.left;
				}
			}
		}

		//GroundCheck
		if(forceCheck || -vecToCheck.y >= -NEAR_ZERO || (HasFlag(CollidedSurface.Ground) && IsJumping())){

			surfaces |= CheckGround (envCheckDistance);
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
	}*/
}
