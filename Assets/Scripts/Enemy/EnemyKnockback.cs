using UnityEngine;
using System.Collections;

public class EnemyKnockback : MonoBehaviour {
	
	public bool knockbackLeft = false;
	public bool knockbackRight = false;
	public float knockbackSpeed = 10f;
	public float knockupSpeed = 10f;

	private Rigidbody2D body;


	// Use this for initialization
	void Start () {
		body = transform.root.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(knockbackLeft){
			body.velocity = new Vector2 (-knockbackSpeed,knockupSpeed);
			knockbackLeft = false;
		}
		else if(knockbackRight){
			body.velocity = new Vector2 (knockbackSpeed,knockupSpeed);
			knockbackRight = false;
		}
	}

	void OnTriggerEnter2D (Collider2D coll2D){
		if(coll2D.gameObject.tag == "Player Projectile"){
			if(transform.position.x > coll2D.transform.position.x){
				knockbackRight = true;
			}
			else if(transform.position.x < coll2D.transform.position.x){
				knockbackLeft = true;
			}
		}
	}
}
