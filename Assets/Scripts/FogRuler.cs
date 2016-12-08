using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class FogRuler : MonoBehaviour {

	public List<GameObject> FoWCloudsInScene;
	public PlayerManager thePlayer;
	public Camera theCamera;

	private List<Collider2D> _collider2D;
	private Renderer _renderer;
	private int originalListSize;

	// Use this for initialization
	void Start () {
	
		thePlayer = FindObjectOfType<PlayerManager>();
		theCamera = Camera.main;
	
			
	}


	// Update is called once per frame
	void Update () {

		if (originalListSize != null) {
			FoWCloudsInScene = new List<GameObject> (GameObject.FindGameObjectsWithTag ("FoW"));
			_collider2D = new List<Collider2D> ();

			FoWCloudsInScene.ToArray ();


			foreach (GameObject FoW in FoWCloudsInScene) {
				_collider2D.Add (FoW.GetComponent<Collider2D> ());
			}

			originalListSize = FoWCloudsInScene.Count;
		}

		for(int i = 0; i < FoWCloudsInScene.Count; i++){
			//if(theCamera.rect.Overlaps(new Rect(theCamera.WorldToViewportPoint(_collider2D[i].transform.position), theCamera.WorldToViewportPoint(_collider2D[i].bounds.size)))){
			if(theCamera.rect.Contains(theCamera.WorldToViewportPoint(_collider2D[i].bounds.min))||
				theCamera.rect.Contains(theCamera.WorldToViewportPoint(_collider2D[i].bounds.max))||
				theCamera.rect.Contains(theCamera.WorldToViewportPoint(new Vector2(_collider2D[i].bounds.min.x,_collider2D[i].bounds.max.y)))||
				theCamera.rect.Contains(theCamera.WorldToViewportPoint(new Vector2(_collider2D[i].bounds.max.x,_collider2D[i].bounds.min.y)))

			 ){
				//NOT OPTIMAL MAY CAUSE FRAME RATE TO TANK
				Destroy(_collider2D[i].gameObject);
				_collider2D.Clear();
				FoWCloudsInScene.Clear();
				return;
			}
						
			}

		}
}
