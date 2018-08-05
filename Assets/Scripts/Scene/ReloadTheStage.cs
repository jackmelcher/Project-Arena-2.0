using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReloadTheStage : MonoBehaviour {

	private PlayerHealthManager checkHeatlh;
	
	void Start (){
		checkHeatlh = GameObject.Find("Player Character").GetComponent<PlayerHealthManager>();
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape))
			SceneManager.LoadScene("SelectionMenuTest");
			//Application.LoadLevel("SelectionMenuTest");

		if(checkHeatlh.health <= 0)
			Invoke("ReloadGame",2f);
	}

	void OnTriggerEnter2D(Collider2D trigger){
		if(trigger.gameObject.tag == "Player")
			Invoke("ReloadGame",2f);
	}


	void ReloadGame(){			
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		//Application.LoadLevel(Application.loadedLevel);
	}
}
