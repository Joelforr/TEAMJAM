using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	public float speed = 1.0f;
	public float jumpSpeed = 1.0f;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)){
			transform.position -= new Vector3(speed, 0, 0);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)){
			transform.position += new Vector3(speed, 0, 0);
		}
		if (Input.GetKeyDown(KeyCode.Space)){
			transform.position += new Vector3(0, jumpSpeed, 0);
		}
	}
}
