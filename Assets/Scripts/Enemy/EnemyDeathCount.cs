using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyDeathCount : MonoBehaviour {

	public int counter = 0;
	
	// Update is called once per frame
	void Update () {

		// GuiText is obsolete
		//GetComponent<GUIText>().text = "Score: " + counter;

		if(counter >= 9000)
			Invoke ("exit",3.0f);

	}

	void exit()
	{
		SceneManager.LoadScene("SelectionMenuTest");
		//Application.LoadLevel("SelectionMenuTest");
	}
}
