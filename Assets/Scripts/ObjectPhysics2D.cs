using UnityEngine;
using System.Collections;

public class ObjectPhysics2D : MonoBehaviour {
	public PlatformerMotor2D motorScript;

	public float fallSpeed = 5f;

	private Collider2D _collider2D;
	private Rigidbody2D _rigidbody2D;
	private bool _ignoreGravity = false;

	void Awake(){
		motorScript = FindObjectOfType<PlatformerMotor2D>();
		_collider2D = GetComponent<Collider2D>();
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		_ignoreGravity = IsGrounded();

		if(_ignoreGravity){
			_rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0f);
		}else{
			_rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x,-fallSpeed);
		}
	}

	private bool IsGrounded(){
		return  Physics2D.BoxCast (
			_collider2D.bounds.center,
			_collider2D.bounds.size, 
			0f, 
			Vector3.down,
			.04f,
			motorScript.staticEnvLayerMask);

	}
}
