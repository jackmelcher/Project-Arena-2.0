using UnityEngine;
using System.Collections;

public class HealthDrop : MonoBehaviour {

	public int heartValue = 1;

	private PlayerHealthManager playerHeatlh;

	void Start () {
		playerHeatlh = GameObject.Find("Player Character").GetComponent<PlayerHealthManager>();
	}

	void OnTriggerEnter2D(Collider2D coll2D){
		if(coll2D.gameObject.tag == "Player"){
			if(playerHeatlh.health+heartValue > playerHeatlh.maxHealth){
				playerHeatlh.health = playerHeatlh.maxHealth;
			}
			else
				playerHeatlh.health = playerHeatlh.health + heartValue;
			Destroy(gameObject);
		}
		if (coll2D.gameObject.tag == "Block") {
			GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
		}
	}

}
