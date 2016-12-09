using UnityEngine;
using System.Collections;

public class DustEffect : MonoBehaviour {

	public GameObject leftParticle;
	public GameObject rightParticle;

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.tag == "Breakable"){
			leftParticle.gameObject.SetActive(true);
			rightParticle.gameObject.SetActive(true);
		}
	}
}
