using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Death : MonoBehaviour {
   

    public Scene scene;
    public string _currScreen;
    public GameObject [] CheckPoint;
     
    public Vector3 CheckPos;

    public GameObject Pushable;
    public GameObject Breakable;

    private int blocksLoaded = 0;

    public PlatformerMotor2D PlatformMotor;
    public GameObject player;
        
    void Start()
    {
        //gets checkpoints
        CheckPoint = GameObject.FindGameObjectsWithTag("Checkpoint");

        // gets the string of the current scene
        _currScreen = SceneManager.GetActiveScene().name;
        
           // gets the blocks
        Pushable = GameObject.Find("Pushable");
        Breakable = GameObject.Find("Breakable");

    }
   
    

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.tag == "DeathZone"){
            
            Debug.Log("You're Dead, Love " + collider.gameObject.name);

            // finds and destroys the blocks on each death
            Pushable = GameObject.Find("Pushable");
            Breakable = GameObject.Find("Breakable");
            Destroy(Pushable);
            Destroy(Breakable);

            // sets up a disctionary of scenes
            Dictionary<string, int> sceneDict = new Dictionary<string, int>();
            sceneDict.Add("ChargeLevel", 1);
            sceneDict.Add("GroundSlamLevel", 2);
            sceneDict.Add("Temple", 3);

            //checks to see if the current scene matches the name of a scene in the dictionary
            if (sceneDict.ContainsKey(_currScreen))
            {
                //checks int value of the current scene and loads the appropriate blocks scenes
                int value = sceneDict[_currScreen];
                if (value == 1)
                {
                    if (blocksLoaded > 1)
                        SceneManager.UnloadScene("ChargeBlocks");

                    SceneManager.LoadSceneAsync("ChargeBlocks", LoadSceneMode.Additive);
                    blocksLoaded += 1;
                }
                if (value == 2)
                {
                    if (blocksLoaded > 1)
                        SceneManager.UnloadScene("GSlamBlocks");

                    SceneManager.LoadSceneAsync("GSlamBlocks", LoadSceneMode.Additive);
                    blocksLoaded += 1;
                }
                if (value == 3)
                {
                    if (blocksLoaded > 1)
                        SceneManager.UnloadScene("TempleBlocks");

                    SceneManager.LoadSceneAsync("TempleBlocks", LoadSceneMode.Additive);
                    blocksLoaded += 1;
                }
            }

            
            

            //send to checkpoint
            transform.position = CheckPos;
            PlatformMotor.groundSpeed = 0;
            PlatformMotor.airSpeed = 0;            
            
        }

        if (collider.gameObject.tag == "Checkpoint")
        {
           // Debug.Log("check");
           //set checkpoint
            CheckPos = collider.bounds.center;
        }
	}
}
