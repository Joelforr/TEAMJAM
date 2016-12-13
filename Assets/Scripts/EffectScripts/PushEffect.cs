using UnityEngine;
using System.Collections;

public class PushEffect : MonoBehaviour {

	public GameObject pushParticle;

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.tag == "Pushable"){
			pushParticle.gameObject.SetActive(true);
		}
	}
}
