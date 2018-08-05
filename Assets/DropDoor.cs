using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDoor : MonoBehaviour {

	private Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D coll2D){
		if(coll2D.gameObject.tag == "Player"){
			//Makes Enemies vulnerable once they are on screen
			rigid.gravityScale = 10f;
		}
	}
}
