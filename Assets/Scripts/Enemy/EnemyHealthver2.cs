using UnityEngine;
using System.Collections;

public class EnemyHealthver2 : MonoBehaviour {

	//Base health
	public int maxHealth = 10;
	public int health = 10;
	//regenerate or revert
	public bool regenenable = false;
	public float regendelay = 0.1f;
	public float counter = 0f;

	public int EnergyMultiplier = 1;
	public int FireMultiplier = 1;
	public int IceMultiplier = 1;
	public int LightningMultiplier = 1;
	public int WindMultiplier = 1;

	//Heart drop
	public Rigidbody2D heart;
	[HideInInspector]
	public float chanceRandomHeart;
	public float dropRate = 25f;
	//On hit effect
	public bool hit = false;

	public bool invincible = true;

	public Material mat1;
	public Material mat2;
	private SpriteRenderer enemyImage;

	void Start(){
		enemyImage = GetComponent<SpriteRenderer> ();
	}

	void Update (){		
		if(health <= 0){
			chanceRandomHeart = Random.Range (1f, 100f);
			//spawn hearts
			if(chanceRandomHeart <= dropRate){
				Rigidbody2D heartInstance = Instantiate(heart, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
				heartInstance.velocity = new Vector2(Random.Range (-2f, 2f), 3);
			}
			Destroy(gameObject);
		}
		if(regenenable){
			if(health < maxHealth)
				counter += Time.deltaTime;
			if (counter >= regendelay){
				health = maxHealth;
				counter = 0f;
			}
		}
		if(hit){
			hit = false;
			enemyImage.sharedMaterial = mat2;
			Invoke ("damaged",0.2f);
		}
	}

	void damaged (){
		enemyImage.sharedMaterial = mat1;
	}
	/*
	void OnCollisionEnter2D (Collision2D coll2D){
		if (coll2D.gameObject.tag == "Player Projectile") {
			hit = true;
		}
	}
	*/
}
