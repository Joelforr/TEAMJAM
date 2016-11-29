using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class WorldManager : MonoBehaviour {

	public List<GameObject> gatesWithinScene;
	private Collider2D _collider;
	public PlayerManager thePlayer;

	// Use this for initialization
	void Start () {
		thePlayer = FindObjectOfType<PlayerManager>();
		gatesWithinScene = new List<GameObject>(GameObject.FindGameObjectsWithTag("Gate"));
		//gatesWithinScene = (List<GameObject>)GameObject.FindGameObjectsWithTag("Gate");
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (ShiftValidtyCheck ());
	}

	public bool ShiftValidtyCheck(){
		bool _isValid = true;

		foreach(GameObject gate in gatesWithinScene){
			_collider = gate.GetComponent<Collider2D>();
			if(_collider.bounds.Contains(thePlayer.transform.position)){
				_isValid = false;
				break;
			}
		}

		return _isValid;
	}
}
