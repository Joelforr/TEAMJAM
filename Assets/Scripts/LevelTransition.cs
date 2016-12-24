using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour {

	public int level;

	// Update is called once per frame
	void Update(){
		if(SceneManager.GetActiveScene().buildIndex == 0){
			if(Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.Return)){
				SceneManager.LoadScene (level);
			}
		}
	}

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
