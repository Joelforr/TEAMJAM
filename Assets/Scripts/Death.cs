using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D collider) {
		Debug.Log ("You're Dead, Love " + collider.gameObject.name);
	}
}
