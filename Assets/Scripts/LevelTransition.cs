using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour {

	public int level = 0;

	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player"){
			Debug.Log("Changing to Scene " + level);
			SceneManager.LoadScene(level);
		}
	}
}
