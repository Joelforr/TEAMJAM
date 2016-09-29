using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	public float speed = 1.0f;
	public float jumpSpeed = 1.0f;

//	// Update is called once per frame
//	void Update () {
//		if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)){
//			transform.position -= new Vector3(speed, 0, 0);
//		}
//		if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)){
//			transform.position += new Vector3(speed, 0, 0);
//		}
//		if (Input.GetKeyDown(KeyCode.Space)){
//			transform.position += new Vector3(0, jumpSpeed, 0);
//		}
//	}

	void Update () {
		float horizontal = Input.GetAxis("Horizontal");
		//float vertical = Input.GetAxis("Vertical");
		//Debug.Log(horizontal);
		Vector3 movement = new Vector3(horizontal, 0, 0) * 10;
		//transform.position += movement * Time.deltaTime;
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x + (movement.x * Time.deltaTime), -60, 60),
			Mathf.Clamp(transform.position.y + (movement.y * Time.deltaTime), 0, 10),
			Mathf.Clamp(transform.position.z + (movement.z * Time.deltaTime), -36, 36)
		);
	}
}
