using UnityEngine;
using System.Collections;

public class UnlockHealth : MonoBehaviour {

	private PlayerHealthManager health;
	
	// Use this for initialization
	void Start () {
		health = GameObject.Find("Player Character").GetComponentInChildren<PlayerHealthManager>();
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D unlock)
	{
		if(unlock.gameObject.tag == "Player")
		{
			health.maxHealth += 1;
			health.health = health.maxHealth;
			Destroy (gameObject);
		}
	}
}
