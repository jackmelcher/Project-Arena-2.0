using UnityEngine;
using System.Collections;

public class CharacterController2D : MonoBehaviour
{
	public bool facingRight = true;			// For determining which way the player is currently facing.
	public bool abletojump = true;			// Condition for whether the player should jump.

	[HideInInspector]
	public bool onSlant = false;			//Check if on slanted platform
	
	[HideInInspector]
	public bool attacking = false;			//Check if attacking so player doesnt flip in the middle of an attack
	
	public float moveSpeed = 10f;			// The fastest the player can travel in the x axis.
	public float jumpSpeed = 20f;			// Amount of force added when the player jumps.
	
	Animator anim;
	
	void Start ()
	{
		//Initialize Animator and reset animator booleans
		anim = GetComponent<Animator>();
		anim.SetBool ("isJumping", false);
	}
	
	void FixedUpdate ()
	{
		// Cache the horizontal input for flip check
		float move = Input.GetAxisRaw("Horizontal");
		//Set velocity values to variables
		float horizontalmovement = GetComponent<Rigidbody2D>().velocity.x;
		float verticalmovement = GetComponent<Rigidbody2D>().velocity.y;
		
		//Cache movement for animations
		anim.SetFloat ("hSpeed", Mathf.Abs (horizontalmovement));
		anim.SetFloat ("vSpeed", Mathf.Abs (verticalmovement));
		
		// If the input is moving the player right and the player is facing left flip the player.
		if(move == 1 && !facingRight && !attacking)
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right flip the player.
		else if(move == -1 && facingRight && !attacking)
			Flip();
	}
	
	void Update ()
	{
		//Use axis to determine positive/negative direction.
		float movement = Input.GetAxisRaw("Horizontal");

		if(GetComponent<Rigidbody2D>().velocity.y == 0f && movement == 1 || movement == -1)
			anim.SetBool("isMoving",true);
		else
			anim.SetBool("isMoving",false);
	
		//running
		if(Input.GetButton ("Horizontal"))
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2 (movement*moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
		}
		//stop
		if(Input.GetButtonUp ("Horizontal") && !onSlant) //flat surface
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2 (0, GetComponent<Rigidbody2D>().velocity.y);
		}
		else if(Input.GetButtonUp ("Horizontal") && onSlant) //slants or stairs
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2 (GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y);
		}
		
		//jump
		if(Input.GetButtonDown ("Jump") && abletojump)
		{
			abletojump = false;
			anim.SetBool ("isJumping", true);
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x,jumpSpeed);
		}
	}
	
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnCollisionStay2D (Collision2D Enable)
	{
		//ReEnable jumping while touching floor
		if (Enable.gameObject.tag == "Floor" || Enable.gameObject.tag == "Slant" || Enable.gameObject.tag == "Block") 
		{
			if(GetComponent<Rigidbody2D>().velocity.y < 3)
			{
				abletojump = true;
				anim.SetBool ("isJumping", false);
			}
		}
		//enable onSlant check
		if (Enable.gameObject.tag == "Slant") 
		{
			onSlant = true;
		}
		//Prevent sliding
		if (Enable.gameObject.tag == "Floor" || Enable.gameObject.tag == "Slant" || Enable.gameObject.tag == "Block") 
		{
			if(!Input.GetButton ("Horizontal") && GetComponent<Rigidbody2D>().velocity.y == 0f)
			{
				GetComponent<Rigidbody2D>().velocity = new Vector2(0f,GetComponent<Rigidbody2D>().velocity.y);
			}
		}
	}
	
	void OnCollisionExit2D (Collision2D Disable)
	{
		//Disable Jumping and Dasing while airborne
		if (Disable.gameObject.tag == "Floor" || Disable.gameObject.tag == "Slant" || Disable.gameObject.tag == "Block")
		{
			abletojump = false;
			anim.SetBool ("isJumping", true);
		}
		//disable onSlant check
		if (Disable.gameObject.tag == "Slant")
		{
			onSlant = false;
		}
	}
}