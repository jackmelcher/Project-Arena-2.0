using UnityEngine;
using System.Collections;

public class PlayerControl2D : MonoBehaviour {

	//moving variables
	public bool facingRight = true;			// For determining which way the player is currently facing.
	public float movement; 
	public bool movementEnabled = true;
	public bool abletojump = false;			// Condition for whether the player should jump.
	public float moveSpeed = 10f;			// The fastest the player can travel in the x axis.
	public float jumpSpeed = 20f;			// Amount of force added when the player jumps.
	public float maxFallSpeed = -30f;

	//Attacking variables
	public bool attacking = false;			//Check if attacking so player doesnt flip in the middle of an attack

	//Scripts
	private Rigidbody2D playerBody;
	private	Animator anim;

	void Start (){
		playerBody = transform.root.GetComponent<Rigidbody2D>();
		anim = transform.root.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		//Use axis to determine positive/negative direction.
		movement = Input.GetAxisRaw("Horizontal");
		anim.SetFloat ("hSpeed", movement);
		if(movementEnabled){
			//Player direction
			Vector3 mousePosition = Input.mousePosition;
			mousePosition.z = 0f;
			mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
			if(mousePosition.x > transform.position.x && !facingRight){
				Flip();
			}
			else if(mousePosition.x < transform.position.x && facingRight){
				Flip();
			}
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
		//jump
		if(Input.GetButtonDown ("Jump") && abletojump){
			abletojump = false;
			playerBody.velocity = new Vector2 (playerBody.velocity.x, jumpSpeed);
		}
		//Jump cancel
		if(Input.GetButtonUp ("Jump") && !abletojump && playerBody.velocity.y > 0){
			playerBody.velocity = new Vector2 (playerBody.velocity.x, 0);
		}
	}
	
	void OnCollisionEnter2D (Collision2D coll2D){
		if (coll2D.gameObject.tag == "Block") {
			//Prevent sliding
			if(!Input.GetButton("Horizontal")){
				playerBody.velocity = new Vector2(0f,playerBody.velocity.y);
			}
			//ReEnable jumping while touching floor
			if(playerBody.velocity.y == 0f && transform.position.y > coll2D.transform.position.y){
				abletojump = true;
				anim.SetBool("isJumping",false);
			}
		}
	}

	void OnCollisionStay2D (Collision2D coll2D){
		//Collisions with blocks
		if (coll2D.gameObject.tag == "Block") {
			//ReEnable jumping while touching floor
			if(playerBody.velocity.y == 0f && transform.position.y > coll2D.transform.position.y){
				abletojump = true;
				anim.SetBool("isJumping",false);
			}
		}
	}
	
	void OnCollisionExit2D (Collision2D coll2D){
		//Disable Jumping when player becomes airborne at all
		if (coll2D.gameObject.tag == "Block" || coll2D.gameObject.tag == "Slant"){
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
