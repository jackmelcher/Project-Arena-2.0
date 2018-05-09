using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour {
	
	//Enemy Properties
	[HideInInspector]
	public bool facingRight = false;
	public bool activeFlipping = false;

	public float speedX = 3f;
	public float speedY = 3f;

	public bool counterActive = true;
	public float counter = 0f;
	public float counterDelay = 10f;

	public float moveCounter = 0f;
	public float moveSwitch = 0f; 

	public float chargeDuration = 10f;

	//In Camera Check
	public bool inView = false;

	//Move Options
	public int randomNumber = 0;

	//public bool charge = false;
	public float laserRate = 0.1f;

	public bool trigger1 = true;
	public bool trigger2 = true;
	public bool trigger3 = true;


	//Components
	private Rigidbody2D rigid;
	private EnemyHealthver2 health;
	private Component[] fire;
	private Transform player;

	//For resetting
	private float positionX;
	private float positionY;
	private float resetTime = 30f;
	private float resetCounter;
	private bool resetOn;
	
	void Start (){
		rigid = GetComponent<Rigidbody2D> ();
		health = GetComponent<EnemyHealthver2>();
		fire = GetComponentsInChildren<BossFireBullet>();
		player = GameObject.Find("Player Character").GetComponent<Transform>();

		health.invincible = false;

		positionX = transform.position.x;
		positionY = transform.position.y;
	}

	// Update is called once per frame
	void Update () {
		if (activeFlipping) {
			if (transform.position.x > player.transform.position.x && facingRight) {
				Flip ();
			}
			if (transform.position.x < player.transform.position.x && !facingRight) {
				Flip ();
			}
		}

		//Activate Trigger Move
		if(trigger1 && health.health <= 0.7*health.maxHealth){
			trigger1 = false;
			counterActive = false;
			counter = 0f;

			Invoke ("TriggerMove",1f);
			Invoke ("reactivateCounter",5f);
		}
		if(trigger2 && health.health <= 0.4*health.maxHealth){
			trigger2 = false;
			counterActive = false;
			counter = 0f;

			Invoke ("TriggerMove",1f);
			Invoke ("reactivateCounter",5f);
		}
		if(trigger3 && health.health <= 0.1*health.maxHealth){
			trigger3 = false;
			counterActive = false;
			counter = 0f;

			Invoke ("TriggerMove",1f);
			Invoke ("reactivateCounter",5f);
		}
		else if(counterActive){
			counter += Time.deltaTime;
			if(counter >= counterDelay){
				counter = 0f;
				counterActive = false;

				//Move 1
				if(moveCounter <= 1f){
					//Turret Shots
					randomNumber = Random.Range(1,30);
					
					foreach(BossFireBullet child in fire){
						if(child.gameObject.name.Contains("Turret")){
							if(randomNumber >= 1 && randomNumber <= 10 && !child.gameObject.name.Contains("(1)")){
								child.Invoke("Fire",0f);
								
							}
							if(randomNumber >= 11 && randomNumber <= 20 && !child.gameObject.name.Contains("(2)")){
								child.Invoke("Fire",0f);
								
							}
							if(randomNumber >= 21 && randomNumber <= 30 && !child.gameObject.name.Contains("(3)")){
								child.Invoke("Fire",0f);
								
							}
						}					
					}
					counterActive = true;
					moveCounter += 1f;
				}
				//Move 2
				else if(moveCounter >= 2f && moveSwitch == 0f){
					//Rocket Launcher
					foreach(BossFireBullet child in fire){
						if(child.gameObject.name == "BackRocket"){
							child.Invoke("Fire",0f);
														
						}
					}
					counterActive = true;
					counter = -1f;
					moveCounter = 0f;
					moveSwitch = 1f;
				}
				//Move 3
				else if(moveCounter >= 2f && moveSwitch == 1f){
					//Laser
					foreach(BossFireBullet child in fire){
						if(child.gameObject.name == "Laser"){
							child.InvokeRepeating("Fire", 0f, laserRate);
						}
					}
					counterActive = true;
					moveCounter = 0f;
					moveSwitch = 0f;
				}
			}
		}
				




		//Resetting
		if(resetOn){
			resetCounter += Time.deltaTime;
			if(resetCounter >= resetTime){
				inView = false;
				transform.position = new Vector3(positionX,positionY,0f);
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

	void OnTriggerExit2D (Collider2D coll2D){
		if (coll2D.gameObject.tag == "MainCamera") {
			//Makes Enemies invincible when off screen and start the reset
			health.invincible = true;
			resetOn = true;
		}
	}

	void OnTriggerEnter2D (Collider2D coll2D){
		if(coll2D.gameObject.name == "MainCamera"){
			//Makes Enemies vulnerable once they are on screen
			health.invincible = false;
			
			if(!inView){
				inView = true;
			}
			
			resetOn = false;
			resetCounter = 0f;
		}
	}

	void TriggerMove (){

		//Turret Shots
		foreach(BossFireBullet child in fire){
			if(child.gameObject.name.Contains("Turret")){
				if(!child.gameObject.name.Contains("(1)")){
					child.Invoke("Fire",0f);
					child.Invoke("Fire",0.1f);
					child.Invoke("Fire",0.2f);
				}
			}				
		}
		//Rocket Launcher
		foreach(BossFireBullet child in fire){
			if(child.gameObject.name == "BackRocket"){
				child.Invoke("Fire",1f);
				child.Invoke("Fire",1.25f);
				child.Invoke("Fire",1.5f);

			}
		}
		//Laser
		foreach(BossFireBullet child in fire){
			if(child.gameObject.name == "Laser"){
				child.InvokeRepeating("Fire", 4f, laserRate);
			}
		}
	}
	void reactivateCounter(){
		counterActive = true;
	}
	/*
	//The functions that control the boss's moveset
	//Charge functions
	void Charge(){
		rigid.velocity = new Vector2 (speedX,0f);
		Invoke ("ResetCharge",chargeDuration);
	}
	void ResetCharge(){
		rigid.velocity = new Vector2 (-speedX/2,0f);
		Invoke ("Stationary",chargeDuration*2);
	}
	void Stationary(){
		rigid.velocity = new Vector2 (0f,0f);
		counterActive = true;
	}
	*/
}
