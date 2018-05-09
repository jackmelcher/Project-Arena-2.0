using UnityEngine;
using System.Collections;

public class ReloadTheStage : MonoBehaviour {

	private PlayerHealthManager checkHeatlh;
	
	void Start (){
		checkHeatlh = GameObject.Find("Player Character").GetComponent<PlayerHealthManager>();
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape))
			Application.LoadLevel("SelectionMenuTest");

		if(checkHeatlh.health <= 0)
			Invoke("ReloadGame",2f);
	}

	void OnTriggerEnter2D(Collider2D trigger){
		if(trigger.gameObject.tag == "Player")
			Invoke("ReloadGame",2f);
	}


	void ReloadGame(){			
		Application.LoadLevel(Application.loadedLevel);
	}
}
