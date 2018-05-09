using UnityEngine;
using System.Collections;

public class PlayerHealthManager : MonoBehaviour {
	
	public float health=1;
	public float maxHealth=1;

	public bool knockbackLeft = false;
	public bool knockbackRight = false;
	public float knockbackSpeed = 10f;
	public float knockupSpeed = 10f;

	public bool invincible = false;
	public bool hit = false;
	public float invincibilityTime = 2f;
	
	private PlayerControl2D playerControl;
	private SpriteRenderer playerImage;
	private Rigidbody2D playerBody;
	private FireBulletver2 fire;

	private BoxCollider2D hitbox;

	private PixelPerfect mainCamera;

	public Material mat1;
	public Material mat2;

	void Start(){
		playerControl = transform.root.GetComponent<PlayerControl2D>();
		playerImage = transform.root.GetComponent<SpriteRenderer>();
		playerBody = transform.root.GetComponent<Rigidbody2D>();
		fire = transform.root.GetComponentInChildren<FireBulletver2>();
		hitbox = transform.Find("Hitbox").GetComponent<BoxCollider2D>();

		maxHealth = transform.root.GetComponentInChildren<Armor>().maxArmor;
		health = maxHealth;
	}
	
	void Update (){
		//Prevent exceeding max health
		if(health > maxHealth)
			health = maxHealth;
		//When you die
		if(health <= 0){
			health = 0;
			playerImage.enabled = false;
			playerControl.enabled = false;
			playerBody.velocity = new Vector2(0f,0f);
			playerBody.gravityScale = 0;

			fire.enabled = false;
			invincible = true;

			Destroy(gameObject);
		}
		

		if(hit && !invincible){
			//Flash
			playerControl.enabled = false;
			playerImage.sharedMaterial = mat2;
			hitbox.enabled = false;
			Invoke ("invincibility",0.2f);
			invincible = true;
			if(knockbackLeft){
				playerBody.velocity = new Vector2 (-knockbackSpeed,knockupSpeed);
				knockbackLeft = false;
			}
			else if(knockbackRight){
				playerBody.velocity = new Vector2 (knockbackSpeed,knockupSpeed);
				knockbackRight = false;
			}
		}
	}

	void invincibility (){
		playerImage.sharedMaterial = mat1;
		playerControl.enabled = true;
		hit = false;
		playerImage.color = new Color (1f, 1f, 1f, 0.5f);
		Invoke ("InvincibiltyEnd",invincibilityTime);
	}
	
	void InvincibiltyEnd (){
		playerImage.color = new Color (1f, 1f, 1f, 1f);
		hitbox.enabled = true;
		invincible = false;
	}
}