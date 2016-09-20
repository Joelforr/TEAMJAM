using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	//PHYSICS VARIABLES
	private Vector2 moveDirection;
	public float moveSpeed;

	public float jumpForce;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = .2f;
	public LayerMask whatIsGround;

	//public float distToGround;
	//public float gravity = 9.8f;

	//PLAYER VARIABLES
	private Collider2D playerCollider;
	private Rigidbody2D playerRigidbody;

	// Use this for initialization
	void Start () {
		playerCollider = GetComponent<Collider2D> ();
		playerRigidbody = GetComponent<Rigidbody2D> ();

		//get distance to ground
		//distToGround = playerCollider.bounds.extents.y;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		//***GROUNDED***
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

		//***MOVEMENT***
		moveDirection.x = Input.GetAxis("Horizontal") * moveSpeed;
		playerRigidbody.velocity = new Vector2 (moveDirection.x, playerRigidbody.velocity.y);

		//***JUMPING***
		if(grounded && Input.GetButtonDown("Jump")){
			playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce);
			}

		/*//subject to gravity
		if(moveDirection.y <=-10){
			moveDirection.y = -10;
		}
		else{
			moveDirection.y -= gravity * Time.deltaTime;
		}*/
			
	}

	/*private bool IsGrounded() {
		return Physics2D.Raycast (transform.position, Vector2.down, distToGround + 0.1f);
	}*/
}
