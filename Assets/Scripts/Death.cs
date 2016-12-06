using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour {

    public Scene scene;
    public GameObject [] CheckPoint;
     
    public Vector3 CheckPos;
        
    void Start()
    {
        CheckPoint = GameObject.FindGameObjectsWithTag("Checkpoint");
    }
   
    
	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.tag == "DeathZone"){
            
            Debug.Log ("You're Dead, Love " + collider.gameObject.name);

            //send to checkpoint
           transform.position = CheckPos;

            
        }

        if (collider.gameObject.tag == "Checkpoint")
        {
           // Debug.Log("check");
           //set checkpoint
            CheckPos = collider.bounds.center;
        }
	}
}
