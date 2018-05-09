using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour {

	public int bodyDamage = 2;
	public bool suicide = false;

	private BoxCollider2D hitbox;

	// Use this for initialization
	void Start () {
		hitbox = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D coll2D){
		if(coll2D.gameObject.tag == "Player" && coll2D.GetComponent<PlayerHealthManager>() != null){
			if(!coll2D.GetComponent<PlayerHealthManager>().invincible){
				coll2D.GetComponent<PlayerHealthManager>().health -= bodyDamage;
				coll2D.GetComponent<PlayerHealthManager>().hit = true;
				if(transform.position.x > coll2D.transform.position.x){
					coll2D.GetComponent<PlayerHealthManager>().knockbackLeft = true;
				}
				else if(transform.position.x < coll2D.transform.position.x){
					coll2D.GetComponent<PlayerHealthManager>().knockbackRight = true;
				}
			}
			if(suicide){
				Destroy(transform.root.gameObject);
			}
			//makes it so the player takes damage if remains in enemy's hitbox
			hitbox.enabled = false;
			Invoke("Hitbox",0.1f);
		}
	}
	void Hitbox(){
		hitbox.enabled = true;

	}
}
