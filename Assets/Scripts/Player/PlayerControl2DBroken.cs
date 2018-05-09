using UnityEngine;
using System.Collections;

public class PlayerControl2DBroken : MonoBehaviour {
	
	//moving variables
	public bool facingRight = true;			// For determining which way the player is currently facing.
	public bool movementEnabled = true;
	public bool abletojump = false;			// Condition for whether the player should jump.
	public float moveSpeed = 10f;			// The fastest the player can travel in the x axis.
	public float jumpSpeed = 20f;			// Amount of force added when the player jumps.
	public float maxFallSpeed = -30f;
	//Attacking variables
	public bool attacking = false;			//Check if attacking so player doesnt flip in the middle of an attack
	
	/*
	//Wall Jump variables
	public bool abletowalljump = false;
	public float walljumpduration = 0.2f;
	public float wallslideSpeed = 20f;
	 */
	
	//Scripts
	//private PlayerHealthManager playerHealth;
	private Rigidbody2D playerBody;
	private	Animator anim;
	
	void Start (){
		//playerHealth = transform.root.GetComponent<PlayerHealthManager>();
		playerBody = transform.root.GetComponent<Rigidbody2D>();
		anim = transform.root.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		//Use axis to determine positive/negative direction.
		float movement = Input.GetAxisRaw("Horizontal");
		anim.SetFloat ("hSpeed", movement);
		if(movementEnabled){
			if(movement == 1 && !facingRight){
				Flip();
			}
			else if(movement == -1 && facingRight){
				Flip();
			}
			/*
			else if(movement == 1 && facingRight && abletowalljump){
				Flip();
			}
			else if(movement == -1 && !facingRight && abletowalljump){
				Flip();
			}
			*/
			//running
			if(movement != 0){
				playerBody.velocity = new Vector2 (movement*moveSpeed, playerBody.velocity.y);
				anim.SetBool("isMoving",true);
			}
			//stop
			if(movement == 0){
				playerBody.velocity = new Vector2 (0, playerBody.velocity.y);
				anim.SetBool("isMoving",false);
			}
		}
		/*
		//walljump
		if(Input.GetButton ("Horizontal") && Input.GetButtonDown ("Jump") && abletowalljump && movement != 0){
			playerBody.velocity = new Vector2 (-1*movement*moveSpeed, jumpSpeed);
			movementEnabled = false;
			anim.SetBool("isJumping",true);
			Invoke("reenable",walljumpduration);
			abletojump = false;
		}
		*/
		//jump
		if(Input.GetButtonDown ("Jump") && abletojump){
			abletojump = false;
			playerBody.velocity = new Vector2 (playerBody.velocity.x, jumpSpeed);
		}
		/*
		//wallslide
		else if (Input.GetButton ("Horizontal") && abletowalljump && playerBody.velocity.y < 0f && movement != 0) {
			playerBody.velocity = new Vector2 (playerBody.velocity.x, wallslideSpeed);
		}
		//maxfallspeed
		if(playerBody.velocity.y <= maxFallSpeed){
			playerBody.velocity = new Vector2(playerBody.velocity.x, maxFallSpeed);
		}
		*/
	}
	
	void OnTriggerEnter2D (Collider2D coll2D){
		
	}
	
	void OnTriggerStay2D (Collider2D coll2D){
		//Check for wallsliding and walljumping
		if (coll2D.gameObject.tag == "Block") {
			/*
			abletowalljump = true;

			if(Input.GetButton ("Horizontal")){
				anim.SetBool("isWallSliding",true);
			}
			else if(movement == 0){
				abletowalljump = false;
				anim.SetBool("isWallSliding",false);
			}
			if(playerBody.velocity.y == 0f){
				abletowalljump = false;
				anim.SetBool("isWallSliding",false);
			}
			*/
			
		}
	}
	void OnTriggerExit2D(Collider2D coll2D){
		if (coll2D.gameObject.tag == "Block") {
			/*
			abletowalljump = false;
			anim.SetBool("isWallSliding",false);
			*/
		}
	}
	void OnCollisionEnter2D (Collision2D coll2D){
		if (coll2D.gameObject.tag == "Block") {
			//Prevent sliding
			if(!Input.GetButton("Horizontal")){
				playerBody.velocity = new Vector2(0f,playerBody.velocity.y);
			}
		}
	}
	
	void OnCollisionStay2D (Collision2D coll2D){
		//Collisions with blocks
		if (coll2D.gameObject.tag == "Block") {
			//ReEnable jumping while touching floor
			if(playerBody.velocity.y == 0f){
				abletojump = true;
				anim.SetBool("isJumping",false);
			}
		}
	}
	
	void OnCollisionExit2D (Collision2D coll2D){
		//Disable Jumping when player becomes airborne at all
		if (coll2D.gameObject.tag == "Block"){
			abletojump = false;
			anim.SetBool("isMoving",false);
			if(playerBody.velocity.y != 0f)
				anim.SetBool("isJumping",true);
		}
	}
	//For changing the visual direction of the player
	void Flip (){
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	//Allow player movement control after walljumping
	void reenable(){
		movementEnabled = true;
	}
}
