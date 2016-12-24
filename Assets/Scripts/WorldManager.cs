using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour {



	public class _Scene{
		public string sceneName;
		public int sceneNumber;
		public bool sceneCleared;


		public _Scene(string name, int number){
			sceneName = name;
			sceneNumber = number;
			sceneCleared = false;
		}
	}


	public static WorldManager instance = null;

	public _Scene[] gameScene;

	public Dictionary<int,bool> sceneDict;
	public List<int> levelsCompletedList;
	public List<GameObject> gatesWithinSceneList;
	public PlayerManager thePlayer;

	public int numOfScenes;
	private Collider2D _gateCollider2D;


	void Awake()
	{
		if(instance == null){
			instance = this;
		}else if (instance != this){
			Destroy(gameObject);
		}
			


		/*gameScene = new _Scene[SceneManager.sceneCountInBuildSettings];
		for(int i = 0; i < gameScene.Length; i++){
			gameScene [i] = new _Scene("xxxxx" + i.ToString(), i);
		}
		print (gameScene [1].sceneName +" " + gameScene[2].sceneName + " " + gameScene[2].sceneCleared );
		*/


		sceneDict = new Dictionary<int,bool>();
		numOfScenes = SceneManager.sceneCountInBuildSettings;

		for(int j = 0; j < 0; j++){
			sceneDict.Add (j, false);
		}

		DontDestroyOnLoad(gameObject);
	}


	// Use this for initialization
	void Start () {
		thePlayer = FindObjectOfType<PlayerManager>();
		gatesWithinSceneList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Gate"));
		levelsCompletedList = new List<int>();

	}
	
	// Update is called once per frame
	void Update () {
		if(thePlayer == null){
			thePlayer = FindObjectOfType<PlayerManager>();
		}

		if(Input.GetKeyDown(KeyCode.R)){
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}

		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
		gatesWithinSceneList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Gate"));
	}




	//Check if player should be allowed to change forms
	public bool ShiftValidtyCheck(){
		bool _isValid = true;

		foreach(GameObject gate in gatesWithinSceneList){
			_gateCollider2D = gate.GetComponent<Collider2D>();
			if(_gateCollider2D.bounds.Contains(thePlayer.transform.position)){
				_isValid = false;
				break;
			}
		}

		return _isValid;
	}


	//Update the dictionary with current scene 
	//Call after a level is completeed
	public void UpdateSceneDictionary(){
		sceneDict [SceneManager.GetActiveScene().buildIndex] = true;
		bool thisSceneValue;
		sceneDict.TryGetValue (SceneManager.GetActiveScene ().buildIndex, out thisSceneValue);
		print (thisSceneValue);
	}


	//Add int to levels completeed list to tell the players what levels have been completed
	//Call at the start of a level
	public void AssignPowers(){
		levelsCompletedList.Clear();
		foreach(KeyValuePair<int,bool> entry in sceneDict){
			int key = entry.Key;
			bool value = entry.Value;

			if(value == true){
				levelsCompletedList.Add(key);
			}
		}
	}

}
