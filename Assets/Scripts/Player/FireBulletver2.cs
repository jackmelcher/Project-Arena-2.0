using UnityEngine;
using System.Collections;

public class FireBulletver2 : MonoBehaviour {
	
	//Bullet Features Mode Type
	//Bullets per shot
	//Rate of Fire
	//Charge Shot Recharge Time
	//Damage over TIme
	//Area of Effect Damage
	//Pierce
	//Slow
	//Knockback

	//Bullet Pathing
	public bool standard;
	public float standardSpeedX = 20f;
	public float standardSpeedY = 20f;
	public Rigidbody2D standardBullet;
	public Rigidbody2D standardBulletCharged;

	public bool bounce;
	public float bounceSpeedX = 20f;
	public float bounceSpeedY = 20f;
	public Rigidbody2D bounceBullet;
	public Rigidbody2D bounceBulletCharged;

	public bool wave;
	public float waveSpeedX = 20f;
	public float waveSpeedY = 10f;
	public Rigidbody2D waveBullet;
	public Rigidbody2D waveBulletCharged;

	public bool boomerang;
	public float boomerangSpeedX = 20f;
	public float boomerangSpeedY = 0f;
	public Rigidbody2D boomerangBullet;
	public Rigidbody2D boomerangBulletCharged;
	public float boomerangRotation = 15;

	public bool volley;
	public float volleySpeedX = 20f;
	public float volleySpeedY = 20f;
	public float volleyRateOfFire = 0.3f;
	public float volleyBulletCount = 1f;
	public Rigidbody2D volleyBullet;
	public Rigidbody2D volleyBulletCharged;
	public float volleyRotation = 30;

	//Other parameters
	public float chargeDelayTime = 3.0f;
	public float chargeReset = 3.0f;
	
	public float delay = 0.2f;
	public float rateOfFire = 0.3f;
	
	public bool fireEnabled = true;
	
	public bool Charged = false;

	//Components
	private PlayerControl2D player;
	private Animator anim;
	//private AttackSystem attackSystem;

	void Start(){
		anim = transform.root.gameObject.GetComponent<Animator>();
		player = transform.root.GetComponent<PlayerControl2D>();
		//attackSystem = transform.root.GetComponent<AttackSystem>();
	}

	void Update (){
		/*
		standard = attackSystem.attackType [0];
		bounce = attackSystem.attackType [1];
		volley = attackSystem.attackType [2];
		wave = attackSystem.attackType [3];
		boomerang = attackSystem.attackType [4];
		*/
		//Charging Mechanism
		if(Input.GetButton ("Fire1")){
			chargeDelayTime = chargeDelayTime - Time.deltaTime;
		}
		
		if(Input.GetButtonUp ("Fire1") && chargeDelayTime > 0){
			chargeDelayTime = chargeReset;
		}
		
		if(chargeDelayTime <=0)
			Charged = true;


		//Normal
		if(Input.GetButtonDown("Fire1") && fireEnabled && !player.attacking){
			//Standard Shot
			if(standard){
				standardFire(standardBullet,standardSpeedX,standardSpeedY,0f);
			}
			//boomerang Shot
			if(boomerang){
				standardFire(boomerangBullet,boomerangSpeedX,boomerangSpeedY, 0f);
			}
			//Volley Shot
			if(volley){
				standardFire(volleyBullet,volleySpeedX,volleySpeedY, volleyRotation);
			}
			//wave Shot
			if(wave){
				standardFire(waveBullet, waveSpeedX, waveSpeedY, 0f);
			}
			//Bounce Shot
			if(bounce){
				standardFire(bounceBullet, bounceSpeedX, bounceSpeedY, 0f);
			}

		}
		//Charged
		if(Input.GetButtonUp ("Fire1") && Charged && !player.attacking){
			//Standard Shot
			if(standard){
				standardFire(standardBulletCharged,standardSpeedX,standardSpeedY,0f);
			}
			//boomerang Shot
			if(boomerang){
				standardFire(boomerangBulletCharged,boomerangSpeedX,boomerangSpeedY, 0f);
			}
			//Volley Shot
			if(volley){
				standardFire(volleyBulletCharged,volleySpeedX,volleySpeedY, volleyRotation);
			}
			//wave Shot
			if(wave){
				standardFire(waveBulletCharged, waveSpeedX, waveSpeedY/2, 0f);
			}
			//Bounce Shot
			if(bounce){
				standardFire(bounceBulletCharged, bounceSpeedX, bounceSpeedY, 0f);
			}
		}

		if(!Input.GetButton("Fire1") && Charged){
			Charged = false;
			chargeDelayTime = chargeReset;
		}

	}

	void standardFire(Rigidbody2D bullet, float bulletSpeedX, float bulletSpeedY, float rotation){
		anim.SetTrigger("RangeAttack");
		anim.SetBool("isAttacking",true);
		
		fireEnabled = false;
		player.attacking = true;
		
		if(player.facingRight){
			Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2(bulletSpeedX, bulletSpeedY);
		}
		else{
			Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0,180,0))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2(-1*bulletSpeedX, bulletSpeedY);
		}
		
		Invoke ("fireTrue", rateOfFire);
		Invoke ("endAttack",delay);
		
		Charged = false;
		chargeDelayTime = chargeReset;


	}

	void multiFire(Rigidbody2D bullet, float bulletSpeedX, float bulletSpeedY, float rateOfFire, float rotation, float positionOffsetY){
		anim.SetTrigger("RangeAttack");
		anim.SetBool("isAttacking",true);
		
		fireEnabled = false;
		player.attacking = true;

		//On the Ground
		if(player.abletojump){
			if(!Charged){
				if(player.facingRight){
					Rigidbody2D bulletInstance1 = Instantiate(bullet, transform.position = new Vector3(transform.position.x, transform.position.y + positionOffsetY, transform.position.z), Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
					bulletInstance1.velocity = new Vector2(bulletSpeedX, 0f);
					Rigidbody2D bulletInstance2 = Instantiate(bullet, transform.position = new Vector3(transform.position.x, transform.position.y - positionOffsetY*2f, transform.position.z), Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
					bulletInstance2.velocity = new Vector2(bulletSpeedX, 0f);
					Rigidbody2D bulletInstance3 = Instantiate(bullet, transform.position = new Vector3(transform.position.x, transform.position.y + positionOffsetY, transform.position.z), Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
					bulletInstance3.velocity = new Vector2(bulletSpeedX, 0f);
				}
				else{
					Rigidbody2D bulletInstance1 = Instantiate(bullet, transform.position = new Vector3(transform.position.x, transform.position.y + positionOffsetY, transform.position.z), Quaternion.Euler(new Vector3(0,180,0))) as Rigidbody2D;
					bulletInstance1.velocity = new Vector2(-1*bulletSpeedX, 0f);
					Rigidbody2D bulletInstance2 = Instantiate(bullet, transform.position = new Vector3(transform.position.x, transform.position.y - positionOffsetY*2f, transform.position.z), Quaternion.Euler(new Vector3(0,180,0))) as Rigidbody2D;
					bulletInstance2.velocity = new Vector2(-1*bulletSpeedX, 0f);
					Rigidbody2D bulletInstance3 = Instantiate(bullet, transform.position = new Vector3(transform.position.x, transform.position.y + positionOffsetY, transform.position.z), Quaternion.Euler(new Vector3(0,180,0))) as Rigidbody2D;
					bulletInstance3.velocity = new Vector2(-1*bulletSpeedX, 0);
				}
			}
			else{
				if(player.facingRight){
					Rigidbody2D bulletInstance1 = Instantiate(bullet, transform.position = new Vector3(transform.position.x, transform.position.y + positionOffsetY*2f, transform.position.z), Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
					bulletInstance1.velocity = new Vector2(bulletSpeedX, 0f);
					Rigidbody2D bulletInstance2 = Instantiate(bullet, transform.position = new Vector3(transform.position.x, transform.position.y - positionOffsetY*4f, transform.position.z), Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
					bulletInstance2.velocity = new Vector2(bulletSpeedX, 0f);
					Rigidbody2D bulletInstance3 = Instantiate(bullet, transform.position = new Vector3(transform.position.x, transform.position.y + positionOffsetY*2f, transform.position.z), Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
					bulletInstance3.velocity = new Vector2(bulletSpeedX, 0f);
				}
				else{
					Rigidbody2D bulletInstance1 = Instantiate(bullet, transform.position = new Vector3(transform.position.x, transform.position.y + positionOffsetY*2f, transform.position.z), Quaternion.Euler(new Vector3(0,180,0))) as Rigidbody2D;
					bulletInstance1.velocity = new Vector2(-1*bulletSpeedX, 0f);
					Rigidbody2D bulletInstance2 = Instantiate(bullet, transform.position = new Vector3(transform.position.x, transform.position.y - positionOffsetY*4f, transform.position.z), Quaternion.Euler(new Vector3(0,180,0))) as Rigidbody2D;
					bulletInstance2.velocity = new Vector2(-1*bulletSpeedX, 0f);
					Rigidbody2D bulletInstance3 = Instantiate(bullet, transform.position = new Vector3(transform.position.x, transform.position.y + positionOffsetY*2f, transform.position.z), Quaternion.Euler(new Vector3(0,180,0))) as Rigidbody2D;
					bulletInstance3.velocity = new Vector2(-1*bulletSpeedX, 0);
				}
			}
		}
		Invoke ("fireTrue", rateOfFire);
		Invoke ("endAttack",delay);
		
		
		Charged = false;
		chargeDelayTime = chargeReset;
	}

	void volleyFire(Rigidbody2D bullet, float bulletSpeedX, float bulletSpeedY, float rateOfFire, float rotation){
		anim.SetTrigger("RangeAttack");
		anim.SetBool("isAttacking",true);
		
		fireEnabled = false;
		player.attacking = true;
		
		if(player.facingRight){
			Rigidbody2D bulletInstance1 = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0,0,rotation))) as Rigidbody2D;
			bulletInstance1.velocity = new Vector2(bulletSpeedX, bulletSpeedY);
			//Rigidbody2D bulletInstance2 = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0,0,rotation))) as Rigidbody2D;
			//bulletInstance2.velocity = new Vector2(bulletSpeedX+bulletSpeedOffsetY, bulletSpeedY);
		}
		else{
			Rigidbody2D bulletInstance1 = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0,180,rotation))) as Rigidbody2D;
			bulletInstance1.velocity = new Vector2(-1*bulletSpeedX, bulletSpeedY);
			//Rigidbody2D bulletInstance2 = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0,180,rotation))) as Rigidbody2D;
			//bulletInstance2.velocity = new Vector2(-1*bulletSpeedX-bulletSpeedOffsetY, bulletSpeedY);
		}
		
		Invoke ("fireTrue", rateOfFire);
		Invoke ("endAttack",delay);
		
		Charged = false;
		chargeDelayTime = chargeReset;
	}



	
	void fireTrue (){
		fireEnabled = true;
	}
	void endAttack(){
		anim.SetBool("isAttacking", false);
		player.attacking = false;
	}
}
