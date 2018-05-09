using UnityEngine;
using System.Collections;

public class UnlockWeapons : MonoBehaviour {

	//private PlayerControl2D player;
	private FireBullet range;

	// Use this for initialization
	void Start(){
		//player = GameObject.Find("Player Character").GetComponentInChildren<PlayerControl2D>();
		range = GameObject.Find("Player Character").GetComponentInChildren<FireBullet>();
	}
	
	void OnTriggerEnter2D (Collider2D unlock){
		if(unlock.gameObject.tag == "Player"){	
			//player.unlockdbljump = true;
			range.enabled = true;
			Destroy (gameObject);
		}
	}
}
