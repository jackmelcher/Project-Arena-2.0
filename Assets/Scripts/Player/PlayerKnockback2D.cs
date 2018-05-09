using UnityEngine;
using System.Collections;

public class PlayerKnockback2D : MonoBehaviour {

	public float knockbackSpeed = 10f;
	public float knockupSpeed = 10f;

	//private PlayerControl2D playerControl;
	private PlayerHealthManager playerHealth;
	private Rigidbody2D playerBody;

	//private	Animator anim;

	void Start(){
		//playerControl = transform.root.GetComponent<PlayerControl2D>();
		playerHealth = transform.root.GetComponent<PlayerHealthManager>();
		playerBody = transform.root.GetComponent<Rigidbody2D>();
		//anim = transform.root.GetComponent<Animator>();

	}

	void Update(){
	}

	//Knockback and Damage
	void OnTriggerEnter2D (Collider2D coll2D){
		//Knockback 
		if(coll2D.gameObject.tag == "EnemyProjectile" || coll2D.gameObject.tag == "Enemy"){
			if(!playerHealth.invincible && !playerHealth.hit && transform.position.x > coll2D.transform.position.x){
				playerHealth.hit = true;
				playerBody.velocity = new Vector2 (knockbackSpeed,knockupSpeed);
			}
			if(!playerHealth.invincible && !playerHealth.hit && transform.position.x < coll2D.transform.position.x){
				playerHealth.hit = true;
				playerBody.velocity = new Vector2 (-1*knockbackSpeed,knockupSpeed);
			}
		}
	}
}
