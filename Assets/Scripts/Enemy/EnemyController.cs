using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	[HideInInspector]
	public bool facingRight = false;
	public float speedX = 3f;
	public float speedY = 3f;
	public float rate = 2f;
	public float moveTime = 0f;
	public float moveDuration = 0.5f;
	public float jumpTime = 0f;
	public float patrolTime = 0f;

	public bool moveX = false;
	public bool moveY = false;
	public bool followX = false;
	public bool followY = false;
	public bool patrol = false;
	public bool jump = false;
	public bool drop = false;
	public bool slide = false;

	private Rigidbody2D rigid;
	private Transform player;
	private EnemyFireBullet detection;

	void Start(){
		rigid = GetComponent<Rigidbody2D> ();
		player = GameObject.Find("Player Character").GetComponent<Transform>();
		detection = GetComponentInChildren<EnemyFireBullet>();

		moveTime = rate - 1f;
		jumpTime = rate - 1f;
	}

	void Update (){
		if(player != null){
			//Types of Flipping
			//Method 1
			if (followX || jump || followY || moveY) {
				if (transform.position.x > player.transform.position.x && facingRight) {
					Flip ();
				}
				if (transform.position.x < player.transform.position.x && !facingRight) {
					Flip ();
				}
			}
			//Method 2
			if (moveX || patrol) {
				if (rigid.velocity.x < 0f && facingRight) {
					Flip ();
				}
				if (rigid.velocity.x > 0f && !facingRight) {
					Flip ();
				}
			}


			//MoveX
			if(moveX && detection.enable){
				moveTime += Time.deltaTime;
				if (moveTime >= rate) {
					if (transform.position.x > player.transform.position.x) {
						rigid.velocity = new Vector2 (-1 * speedX, speedY);
					}
					if (transform.position.x < player.transform.position.x) {
						rigid.velocity = new Vector2 (speedX, speedY);
					}
					Invoke ("Stop",moveDuration);
					moveTime = 0;
				}
			}
			else{moveTime = 0;}
			//MoveY
			if(moveY && detection.enable){
				if(facingRight){
					rigid.velocity = new Vector2 (rigid.velocity.x, speedY);
				}
				else if(!facingRight){
					rigid.velocity = new Vector2 (rigid.velocity.x, -1*speedY);
				}

			}
			//FollowX
			if(followX && detection.enable){
				if(facingRight && transform.position.x <= player.transform.position.x-1f){
					rigid.velocity = new Vector2 (speedX, rigid.velocity.y);
				}
				else if(!facingRight&& transform.position.x >= player.transform.position.x+1f){
					rigid.velocity = new Vector2 (-1*speedX, rigid.velocity.y);
				}
				else{rigid.velocity = new Vector2 (0f, rigid.velocity.y);}
			}
			else if(!detection.enable){
				rigid.velocity = new Vector2 (0f, rigid.velocity.y);
			}
			//FollowY
			if(followY && detection.enable){
				if(transform.position.y <= player.transform.position.y){
					rigid.velocity = new Vector2 (rigid.velocity.x, speedY);
				}
				else if(transform.position.y >= player.transform.position.y){
					rigid.velocity = new Vector2 (rigid.velocity.x, -1*speedY);
				}
			}
			else if(!detection.enable){
				rigid.velocity = new Vector2 (0f, rigid.velocity.y);
			}
			//Patrol
			if (patrol && detection.enable) {
				rigid.velocity = new Vector2 (speedX, speedY);
				patrolTime += Time.deltaTime;
				if (patrolTime >= rate) {
					speedX = -1*speedX;
					speedY = -1*speedY;
					patrolTime = 0;
				}
			}

			//Bouncing/Jumping
			if (jump && detection.enable) {
				jumpTime += Time.deltaTime;
				if (jumpTime >= rate) {
					if (transform.position.x > player.transform.position.x) {
						rigid.velocity = new Vector2 (-1 * speedX, speedY);
					}
					if (transform.position.x < player.transform.position.x) {
						rigid.velocity = new Vector2 (speedX, speedY);
					}
					jumpTime = 0;
				}
			}
			else{jumpTime = 0;}

			//Drop
			if(drop && detection.enable){
				if(transform.position.y >= player.transform.position.y + 3f){
					if (transform.position.x <= (player.transform.position.x + 0.5f) &&  transform.position.x >= (player.transform.position.x - 0.5f)) {
						rigid.velocity = new Vector2 (0, -speedY);
					}
				}
			}
		}
	}

	void Flip (){
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	void Stop(){
		rigid.velocity = new Vector2 (0f, rigid.velocity.y);
		moveTime = 0f;
	}

	void OnTriggerEnter2D (Collider2D flip){
		if(flip.gameObject.name == "EnemyCollisionBlock"){
			Flip ();
		}
	}

	void OnTriggerExit2D (Collider2D coll2D){
		if (coll2D.gameObject.tag == "MainCamera") {
					
		}
	}

	void OnCollisionEnter2D (Collision2D coll){
		if (coll.gameObject.tag == "Floor" || coll.gameObject.tag == "Block") {
			//Prevents enemy from sliding along floor
			if (!slide) {
				rigid.velocity = new Vector2 (0f, 0f);
			}
		}
	}
}
