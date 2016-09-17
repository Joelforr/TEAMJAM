using UnityEngine;
using System.Collections;

public class DuaeController : MonoBehaviour {

	// *** PLAYER VARIABLES ***
	public GameObject Duae;
	private Collider2D DuaeCollider;
	private Rigidbody2D DuaeRigidbody;

	// *** PHYSICS VARIABLES ***
	public Vector2 moveDirection;

	public LayerMask whatIsGround;
	private float distToGround;

	//*** PLAYER STATE ***
	private bool isUncloaked = false;

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

		//Debug.Log (DuaeRigidbody.velocity.y);
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
		float cloakedMoveSpeed = 7f;
		float cloakedJumpForce = 13f;

		//DuaeRigidbody.drag = 5f;
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
		float uncloakedMoveSpeed = 15f;
		float uncloakedJumpForce = 3f;

		//DuaeRigidbody.gravityScale = 9.8f;
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


}
