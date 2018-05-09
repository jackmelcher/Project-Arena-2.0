using UnityEngine;
using System.Collections;

public class EnemyDeathCount : MonoBehaviour {

	public int counter = 0;
	
	// Update is called once per frame
	void Update () {

		GetComponent<GUIText>().text = "Score: " + counter;

		if(counter >= 9000)
			Invoke ("exit",3.0f);

	}

	void exit()
	{
		Application.LoadLevel("SelectionMenuTest");
	}
}
