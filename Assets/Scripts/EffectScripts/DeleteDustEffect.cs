using UnityEngine;
using System.Collections;

public class DeleteDustEffect : MonoBehaviour {

	public GameObject triggerBox;
	public GameObject particleEffect;
	public GameObject leftDust;
	public GameObject rightDust;

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.tag == "Pushable"){
			particleEffect.gameObject.SetActive(false);
			triggerBox.gameObject.SetActive(false);
			leftDust.gameObject.SetActive(true);
			rightDust.gameObject.SetActive(true);
			AudioSource boom = GetComponent<AudioSource>();
			boom.Play();
		}
	}
}
