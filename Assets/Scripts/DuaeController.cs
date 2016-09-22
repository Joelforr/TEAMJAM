using UnityEngine;
using System.Collections;

public class DuaeController : MonoBehaviour {

	// *** PLAYER VARIABLES ***
	// ************************
	public GameObject Duae;
	private Collider2D DuaeCollider;
	private Rigidbody2D DuaeRigidbody;


	// *** PHYSICS VARIABLES ***
	// *************************
	public Vector2 moveDirection;

	public LayerMask whatIsGround;
	private float distToGround;

	//--CLOAKED--
	public float cloakedMoveSpeed = 7f;
	public float cloakedJumpForce = 10f;

	//--UNCLOAKED--
	public float uncloakedMoveSpeed = 5f;
	public float uncloakedMinMoveSpeed = 5f;
	public float uncloakedMaxMoveSpeed = 15f;
	public float uncloakedJumpForce = 3f;
	public float uncloakedIncreaseInterval;
	public float uncloakedIntervalTimer;
	public float uncloakedIncreaseAmount;
	 


	//*** PLAYER STATES ***
	//*********************
	public bool isUncloaked = false;


	//** DEBUGING VARIABLES**
	//public float extradist;


	// Use this for initialization
	void Start () {
		DuaeCollider = GetComponent<Collider2D>();
		DuaeRigidbody = GetComponent<Rigidbody2D>();

		distToGround = DuaeCollider.bounds.extents.y;


	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftControl)){
			isUncloaked = !isUncloaked;
		}

		//** DEBUGGING **

		//**Debuging Ground Check
		//float dist = (distToGround + extradist);
		//Debug.DrawRay (transform.position, Vector3.down * dist);
		//Debug.Log (IsGrounded ());

		Debug.Log (DuaeRigidbody.velocity.y);
		//Debug.Log (DuaeRigidbody.drag);

	
	}


	void FixedUpdate(){
		if(isUncloaked == false){
			Cloaked ();
		}else
			if(isUncloaked == true){
			Uncloaked ();
			}	
	}


	// *** MOVEMENT WHEN CLOAKED ***
	public void Cloaked(){

		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),LayerMask.NameToLayer("DarkWorld"), true);
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),LayerMask.NameToLayer("LightWorld"), false);

		DuaeRigidbody.freezeRotation = true;
		moveDirection.x = Input.GetAxis("Horizontal") * cloakedMoveSpeed;
		DuaeRigidbody.velocity = new Vector2(moveDirection.x, DuaeRigidbody.velocity.y);

		if(IsGrounded() && Input.GetKey(KeyCode.Space)){
			DuaeRigidbody.velocity = new Vector2 (DuaeRigidbody.velocity.x, cloakedJumpForce);
		}

		if(DuaeRigidbody.velocity.y < 0){
			DuaeRigidbody.drag = 1.5f;
		}

		if(IsGrounded()){
			DuaeRigidbody.drag = 0f;
		}
	}

	// *** MOVEMENT WHEN UNCLOAKED ***	
	public void Uncloaked(){

		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),LayerMask.NameToLayer("DarkWorld"), false);
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),LayerMask.NameToLayer("LightWorld"), true);

		DuaeRigidbody.drag = -.5f;
		DuaeRigidbody.freezeRotation = true;
		moveDirection.x = Input.GetAxis("Horizontal") * uncloakedMoveSpeed;

		DuaeRigidbody.velocity = new Vector2(moveDirection.x, DuaeRigidbody.velocity.y);


		if(IsGrounded() && Input.GetKey(KeyCode.Space)){
			DuaeRigidbody.velocity = new Vector2 (DuaeRigidbody.velocity.x, uncloakedJumpForce);
		}
	}




















	// *** GROUND CHECK FUNCTION ***
	public bool IsGrounded(){
		return Physics2D.Raycast(transform.position, Vector2.down, distToGround + .1f, whatIsGround);

	}


	// *** INCREASE SPEED FUNCTION ****
	public float IncreaseSpeed(float subjectMoveSpeed, float increaseInterval, float intervalTimer, float increaseAmount){
		if(intervalTimer <= 0){
			subjectMoveSpeed += increaseAmount;
			intervalTimer = increaseInterval;
		}else
			if(intervalTimer > 0){
				intervalTimer = intervalTimer -Time.deltaTime;
			}
		print ("ucm " + subjectMoveSpeed);
		print ("it " + intervalTimer);
		return subjectMoveSpeed;
	}

	//STUFF FOR TESTING JUMP INTERPOLATIOIN
	/*public void Fall(){
		//DuaeRigidbody.velocity = new Vector2 (DuaeRigidbody.velocity.x, 0f);
		//DuaeRigidbody.gravityScale = 1f;
	}*/

}
