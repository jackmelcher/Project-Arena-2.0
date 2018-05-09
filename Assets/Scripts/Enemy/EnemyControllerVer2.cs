﻿using UnityEngine;
using System.Collections;

public class EnemyControllerVer2 : MonoBehaviour {
	
	[HideInInspector]
	//Enemy Properties
	public bool facingRight = false;
	public float speedX = 3f;
	public float speedY = 3f;
	public float rate = 2f;
	public float moveTime = 0f;
	public float moveDuration = 0.5f;
	public float jumpTime = 0f;
	public float patrolTime = 0f;
	//In Camera Check
	public bool inView = false;
	//Movement Options
	public bool moveX = false;
	public bool moveY = false;
	public bool followX = false;
	public bool followY = false;
	public bool patrol = false;
	public bool jump = false;
	public bool drop = false;
	public bool slide = false;
	//Components
	private Rigidbody2D rigid;
	private EnemyHealthver2 health;
	private EnemyFireBullet fire;
	private Transform player;
	//For resetting
	private float positionX;
	private float positionY;
	private float resetTime = 30f;
	private float resetCounter;
	private bool resetOn;

	void Start(){
		rigid = GetComponent<Rigidbody2D> ();
		health = GetComponent<EnemyHealthver2>();
		fire = GetComponentInChildren<EnemyFireBullet> ();
		player = GameObject.Find("Player Character").GetComponent<Transform>();

		moveTime = rate - 0.5f;
		jumpTime = rate - 0.5f;

		positionX = transform.position.x;
		positionY = transform.position.y;
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
			else if (moveX || patrol) {
				if (rigid.velocity.x < 0f && facingRight) {
					Flip ();
				}
				if (rigid.velocity.x > 0f && !facingRight) {
					Flip ();
				}
			}
			
			if(inView){
				//MoveX
				if(moveX){
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

				//MoveY
				if(moveY){
					if(facingRight){
						rigid.velocity = new Vector2 (rigid.velocity.x, speedY);
					}
					else if(!facingRight){
						rigid.velocity = new Vector2 (rigid.velocity.x, -1*speedY);
					}
					
				}
				//FollowX
				if(followX && inView){
					if(facingRight && transform.position.x <= player.transform.position.x-1f){
						rigid.velocity = new Vector2 (speedX, rigid.velocity.y);
					}
					else if(!facingRight&& transform.position.x >= player.transform.position.x+1f){
						rigid.velocity = new Vector2 (-1*speedX, rigid.velocity.y);
					}
					else{rigid.velocity = new Vector2 (0f, rigid.velocity.y);}
				}

				//FollowY
				if(followY){
					if(transform.position.y <= player.transform.position.y-0.5f){
						rigid.velocity = new Vector2 (rigid.velocity.x, speedY);
					}
					else if(transform.position.y >= player.transform.position.y+0.5f){
						rigid.velocity = new Vector2 (rigid.velocity.x, -1*speedY);
					}
					else{rigid.velocity = new Vector2 (rigid.velocity.x, 0f);}
				}

				//Patrol
				if (patrol) {
					rigid.velocity = new Vector2 (speedX, speedY);
					patrolTime += Time.deltaTime;
					if (patrolTime >= rate) {
						speedX = -1*speedX;
						speedY = -1*speedY;
						patrolTime = 0;
					}
				}
				
				//Bouncing/Jumping
				if (jump) {
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

				//Drop
				if(drop){
					if(transform.position.y >= player.transform.position.y + 3f){
						if (transform.position.x <= (player.transform.position.x + 0.5f) &&  transform.position.x >= (player.transform.position.x - 0.5f)) {
							rigid.velocity = new Vector2 (0, -speedY);
						}
					}
				}
			}
		}

		//Resetting
		if(resetOn){
			resetCounter += Time.deltaTime;
			if(resetCounter >= resetTime){
				inView = false;
				fire.enable = false;
				transform.position = new Vector3(positionX,positionY,0f);
				moveTime = rate - 0.5f;
				jumpTime = rate - 0.5f;
				rigid.velocity = new Vector2 (0f, 0f);
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
	
	void OnTriggerEnter2D (Collider2D coll2D){
		if(coll2D.gameObject.name == "MainCamera"){
			//Makes Enemies vulnerable once they are on screen
			health.invincible = false;

			if(!inView){
				inView = true;
			}

			fire.enable = true;

			resetOn = false;
			resetCounter = 0f;
		}
	}
	
	void OnTriggerExit2D (Collider2D coll2D){
		if (coll2D.gameObject.tag == "MainCamera") {

			//Makes Enemies invincible when off screen
			health.invincible = true;

			resetOn = true;
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