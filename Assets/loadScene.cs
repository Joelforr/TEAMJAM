using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class loadScene : MonoBehaviour {
    public Death _death;
   	// Use this for initialization
	void Start () {
	//_death = GameObject.Find("Basic_Player_Controller_1").GetComponent<Death>();
    }
	
	// Update is called once per frame
	void Update () {

        
        //SceneManager.GetSceneByName(_scene.name);

        if(Input.GetKeyDown("space"))
        {
       //     SceneManager.LoadScene(_death.scene.name);
        }
	}
}
