using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public bool isOtherWorld = false;

	public GameObject thePlayer;
	public GameObject theGameManager;
	private Vector3 lastPlayerPosition;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		lastPlayerPosition = thePlayer.transform.position;

		if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)){
			ShiftWorlds ();
		}
	
	}

	private void ShiftWorlds(){
		DontDestroyOnLoad (thePlayer);
		DontDestroyOnLoad (theGameManager);

		if(!isOtherWorld){
			SceneManager.LoadScene (1);
			isOtherWorld = true;
		}
		else{
			SceneManager.LoadScene (0);
			isOtherWorld = false;
		}
			
		thePlayer.transform.position = lastPlayerPosition;

	}
}
