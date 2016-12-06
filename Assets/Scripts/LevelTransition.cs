using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour {

	public int level;

	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player"){

			if (NiceSceneTransition.instance != null) {
				NiceSceneTransition.instance.LoadScene(level);
			}
			else {
				SceneManager.LoadScene(level);
			}
		}
	}
}
