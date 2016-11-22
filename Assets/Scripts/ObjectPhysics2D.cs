using UnityEngine;
using System.Collections;

public class ObjectPhysics2D : MonoBehaviour {
	public PlatformerMotor2D motorScript;

	public float fallSpeed = 5f;

	private Collider2D _collider2D;
	private Rigidbody2D _rigidbody2D;
	private bool _ignoreGravity = false;
	public RaycastHit2D raycast;

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

		raycast = IsGroundedRC();
		Debug.Log (raycast.point);

		if(_ignoreGravity){
			_rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0f);
			this.tag = "Pushable";
		}else{	
			_rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x,-fallSpeed);
			this.tag = null;
		}
	}

	private bool IsGrounded(){
		return  Physics2D.BoxCast (
			transform.position,
			new Vector2(_collider2D.bounds.size.x*.9f, _collider2D.bounds.size.y *.9f), 
			0f, 
			Vector3.down,
			.1f,
			motorScript.staticEnvLayerMask);


	}

	private RaycastHit2D IsGroundedRC(){
		return  Physics2D.BoxCast (
			transform.position,
			new Vector2(_collider2D.bounds.size.x*.9f, _collider2D.bounds.size.y * .9f), 
			0f, 
			Vector3.down,
			.1f,
			motorScript.staticEnvLayerMask);


	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.black;
		Gizmos.DrawWireCube(transform.position, new Vector2(GetComponent<Collider2D>().bounds.size.x*.9f, GetComponent<Collider2D>().bounds.size.y));

		Gizmos.DrawLine (raycast.centroid, raycast.point);
	}
}
