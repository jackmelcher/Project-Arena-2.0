using UnityEngine;
using System.Collections;

public class FireBullet : MonoBehaviour {
	//Bullet Bodies
	public Rigidbody2D bullet;
	public Rigidbody2D chargedBullet;
	public Rigidbody2D Fire;
	public Rigidbody2D chargedFire;
	public Rigidbody2D Ice;
	public Rigidbody2D chargedIce;
	public Rigidbody2D Lightning;
	public Rigidbody2D chargedLightning;
	public Rigidbody2D Wind;
	public Rigidbody2D chargedWind;
	//Bullet Speeds
	public float bulletSpeed = 20f;
	public float bulletSpeedFireY = 5f;
	public float bulletSpeedIceOffsetX = 5f;
	public float bulletSpeedIceY = 10f;
	public float bulletSpeedWindY = 3f;
	public float bulletSpeedWindOffsetY = 0.5f;

	public float chargeDelayTime = 3.0f;
	public float chargeReset = 3.0f;

	public float delay = 0.2f;
	public float rateOfFire = 0.3f;

	public float energyRateOfFire = 0.1f;
	public float fireRateOfFire = 0.5f;
	public float iceRateOfFire = 0.25f;
	public float lightningRateOfFire = 0.5f;
	public float windRateOfFire = 0.25f;


	public bool fireEnabled = true;
	
	public bool Charged = false;

	private PlayerControl2D player;
	private ElementSystem element;
	private Animator anim;



	void Start(){
		anim = transform.root.gameObject.GetComponent<Animator>();
		player = transform.root.GetComponent<PlayerControl2D>();
		element = transform.root.GetComponent<ElementSystem>();
	}
	
	void Update (){
		//Charging Mechanism
		if(Input.GetButton ("Fire1")){
			chargeDelayTime = chargeDelayTime - Time.deltaTime;
		}
		
		if(Input.GetButtonUp ("Fire1") && chargeDelayTime > 0){
			chargeDelayTime = chargeReset;
		}
		
		if(chargeDelayTime <=0)
			Charged = true;

		//No Element
		if(!element.fire && !element.ice && !element.lightning && !element.wind){	
			if(Input.GetButtonDown("Fire1") && fireEnabled && !player.attacking){

				anim.SetBool("isAttacking",true);
				anim.SetTrigger("RangeAttack");

				fireEnabled = false;
				player.attacking = true;

				if(player.facingRight){
					Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
					bulletInstance.velocity = new Vector2(bulletSpeed, 0);
				}
				else{
					Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0,180,0))) as Rigidbody2D;
					bulletInstance.velocity = new Vector2(-1*bulletSpeed, 0);
				}

				Invoke ("fireTrue", energyRateOfFire);
				Invoke ("endAttack",0.2f);
			}
			//Charged Shot
			if(Input.GetButtonUp ("Fire1") && Charged && !player.attacking){
				anim.SetTrigger("RangeAttack");
				anim.SetBool("isAttacking",true);

				fireEnabled = false;
				player.attacking = true;

				if(player.facingRight){
					Rigidbody2D bulletInstance = Instantiate(chargedBullet, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
					bulletInstance.velocity = new Vector2(bulletSpeed, 0);
				}
				else{
					Rigidbody2D bulletInstance = Instantiate(chargedBullet, transform.position, Quaternion.Euler(new Vector3(0,180,0))) as Rigidbody2D;
					bulletInstance.velocity = new Vector2(-1*bulletSpeed, 0);
				}

				Invoke ("fireTrue", energyRateOfFire);
				Invoke ("endAttack",0.2f);

				Charged = false;
				chargeDelayTime = chargeReset;
			}
		}
		//Fire
		if(element.fire){
			if(Input.GetButtonDown("Fire1") && fireEnabled && !player.attacking){
				anim.SetTrigger("RangeAttack");
				anim.SetBool("isAttacking",true);
				
				fireEnabled = false;
				player.attacking = true;
				
				if(player.facingRight){
					Rigidbody2D bulletInstance = Instantiate(Fire, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
					bulletInstance.velocity = new Vector2(bulletSpeed, bulletSpeedFireY);
				}
				else{
					Rigidbody2D bulletInstance = Instantiate(Fire, transform.position, Quaternion.Euler(new Vector3(0,180,0))) as Rigidbody2D;
					bulletInstance.velocity = new Vector2(-1*bulletSpeed, bulletSpeedFireY);
				}
				
				Invoke ("fireTrue", fireRateOfFire);
				Invoke ("endAttack",0.2f);

			}
			//Charged (Fire)
			if(Input.GetButtonUp ("Fire1") && Charged && !player.attacking){
				anim.SetTrigger("RangeAttack");
				anim.SetBool("isAttacking",true);

				fireEnabled = false;
				player.attacking = true;

				if(player.facingRight){
					Rigidbody2D bulletInstance = Instantiate(chargedFire, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
					bulletInstance.velocity = new Vector2(bulletSpeed, bulletSpeedFireY);
				}
				else{
					Rigidbody2D bulletInstance = Instantiate(chargedFire, transform.position, Quaternion.Euler(new Vector3(0,180,0))) as Rigidbody2D;
					bulletInstance.velocity = new Vector2(-1*bulletSpeed, bulletSpeedFireY);
				}
				
				Invoke ("fireTrue", fireRateOfFire);
				Invoke ("endAttack",0.2f);

				Charged = false;
				chargeDelayTime = chargeReset;
			}
		}
		//Ice
		if(element.ice){
			if(Input.GetButtonDown("Fire1") && fireEnabled && !player.attacking){
				anim.SetTrigger("RangeAttack");
				anim.SetBool("isAttacking",true);
				
				fireEnabled = false;
				player.attacking = true;
				
				if(player.facingRight){
					Rigidbody2D bulletInstance1 = Instantiate(Ice, transform.position, Quaternion.Euler(new Vector3(0,0,30))) as Rigidbody2D;
					bulletInstance1.velocity = new Vector2(bulletSpeed, bulletSpeedIceY);
					Rigidbody2D bulletInstance2 = Instantiate(Ice, transform.position, Quaternion.Euler(new Vector3(0,0,30))) as Rigidbody2D;
					bulletInstance2.velocity = new Vector2(bulletSpeed+bulletSpeedIceOffsetX, bulletSpeedIceY);
				}
				else{
					Rigidbody2D bulletInstance1 = Instantiate(Ice, transform.position, Quaternion.Euler(new Vector3(0,180,30))) as Rigidbody2D;
					bulletInstance1.velocity = new Vector2(-1*bulletSpeed, bulletSpeedIceY);
					Rigidbody2D bulletInstance2 = Instantiate(Ice, transform.position, Quaternion.Euler(new Vector3(0,180,30))) as Rigidbody2D;
					bulletInstance2.velocity = new Vector2(-1*bulletSpeed-bulletSpeedIceOffsetX, bulletSpeedIceY);
				}
				
				Invoke ("fireTrue", iceRateOfFire);
				Invoke ("endAttack",0.2f);

			}
			//Charged (Ice)
			if(Input.GetButtonUp ("Fire1") && Charged && !player.attacking){
				anim.SetTrigger("RangeAttack");
				anim.SetBool("isAttacking",true);

				fireEnabled = false;
				player.attacking = true;

				if(player.facingRight){
					Rigidbody2D bulletInstance1 = Instantiate(chargedIce, transform.position, Quaternion.Euler(new Vector3(0,0,30))) as Rigidbody2D;
					bulletInstance1.velocity = new Vector2(bulletSpeed, bulletSpeedIceY);
					Rigidbody2D bulletInstance2 = Instantiate(chargedIce, transform.position, Quaternion.Euler(new Vector3(0,0,30))) as Rigidbody2D;
					bulletInstance2.velocity = new Vector2(bulletSpeed+bulletSpeedIceOffsetX, bulletSpeedIceY);
				}
				else{
					Rigidbody2D bulletInstance1 = Instantiate(chargedIce, transform.position, Quaternion.Euler(new Vector3(0,180,30))) as Rigidbody2D;
					bulletInstance1.velocity = new Vector2(-1*bulletSpeed, bulletSpeedIceY);
					Rigidbody2D bulletInstance2 = Instantiate(chargedIce, transform.position, Quaternion.Euler(new Vector3(0,180,30))) as Rigidbody2D;
					bulletInstance2.velocity = new Vector2(-1*bulletSpeed-bulletSpeedIceOffsetX, bulletSpeedIceY);
				}
				
				Invoke ("fireTrue", iceRateOfFire);
				Invoke ("endAttack",0.2f);

				Charged = false;
				chargeDelayTime = chargeReset;
			}
		}
		//Lightning
		if(element.lightning){
			if(Input.GetButtonDown("Fire1") && fireEnabled && !player.attacking){
				anim.SetTrigger("RangeAttack");
				anim.SetBool("isAttacking",true);
				
				fireEnabled = false;
				player.attacking = true;
				
				if(player.facingRight){
					Rigidbody2D bulletInstance = Instantiate(Lightning, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
					bulletInstance.velocity = new Vector2(bulletSpeed, 0f);
				}
				else{
					Rigidbody2D bulletInstance = Instantiate(Lightning, transform.position, Quaternion.Euler(new Vector3(0,180,0))) as Rigidbody2D;
					bulletInstance.velocity = new Vector2(-1*bulletSpeed, 0f);
				}
				
				Invoke ("fireTrue", lightningRateOfFire);
				Invoke ("endAttack",0.2f);

			}
			//Charged (Lightning)
			if(Input.GetButtonUp ("Fire1") && Charged && !player.attacking){
				anim.SetTrigger("RangeAttack");
				anim.SetBool("isAttacking",true);

				fireEnabled = false;
				player.attacking = true;

				if(player.facingRight){
					Rigidbody2D bulletInstance = Instantiate(chargedLightning, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
					bulletInstance.velocity = new Vector2(bulletSpeed, 0f);
				}
				else{
					Rigidbody2D bulletInstance = Instantiate(chargedLightning, transform.position, Quaternion.Euler(new Vector3(0,180,0))) as Rigidbody2D;
					bulletInstance.velocity = new Vector2(-1*bulletSpeed, 0f);
				}
				
				Invoke ("fireTrue", lightningRateOfFire);
				Invoke ("endAttack",0.2f);

				
				Charged = false;
				chargeDelayTime = chargeReset;
			}
		}
		//Wind
		if(element.wind){
			//On the Ground
			if(Input.GetButtonDown("Fire1") && fireEnabled && !player.attacking && player.abletojump){
				anim.SetTrigger("RangeAttack");
				anim.SetBool("isAttacking",true);
				
				fireEnabled = false;
				player.attacking = true;
				
				if(player.facingRight){
					Rigidbody2D bulletInstance1 = Instantiate(Wind, transform.position = new Vector3(transform.position.x, transform.position.y + bulletSpeedWindOffsetY, transform.position.z), Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
					bulletInstance1.velocity = new Vector2(bulletSpeed, 0f);
					Rigidbody2D bulletInstance2 = Instantiate(Wind, transform.position = new Vector3(transform.position.x, transform.position.y - bulletSpeedWindOffsetY*2f, transform.position.z), Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
					bulletInstance2.velocity = new Vector2(bulletSpeed, 0f);
					Rigidbody2D bulletInstance3 = Instantiate(Wind, transform.position = new Vector3(transform.position.x, transform.position.y + bulletSpeedWindOffsetY, transform.position.z), Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
					bulletInstance3.velocity = new Vector2(bulletSpeed, 0f);
				}
				else{
					Rigidbody2D bulletInstance1 = Instantiate(Wind, transform.position = new Vector3(transform.position.x, transform.position.y + bulletSpeedWindOffsetY, transform.position.z), Quaternion.Euler(new Vector3(0,180,0))) as Rigidbody2D;
					bulletInstance1.velocity = new Vector2(-1*bulletSpeed, 0f);
					Rigidbody2D bulletInstance2 = Instantiate(Wind, transform.position = new Vector3(transform.position.x, transform.position.y - bulletSpeedWindOffsetY*2f, transform.position.z), Quaternion.Euler(new Vector3(0,180,0))) as Rigidbody2D;
					bulletInstance2.velocity = new Vector2(-1*bulletSpeed, 0f);
					Rigidbody2D bulletInstance3 = Instantiate(Wind, transform.position = new Vector3(transform.position.x, transform.position.y + bulletSpeedWindOffsetY, transform.position.z), Quaternion.Euler(new Vector3(0,180,0))) as Rigidbody2D;
					bulletInstance3.velocity = new Vector2(-1*bulletSpeed, 0f);
				}
				
				Invoke ("fireTrue", windRateOfFire);
				Invoke ("endAttack",0.2f);

			}
			//Charged (Wind)
			if(Input.GetButtonUp ("Fire1") && Charged && !player.attacking && player.abletojump){
				anim.SetTrigger("RangeAttack");
				anim.SetBool("isAttacking",true);

				fireEnabled = false;
				player.attacking = true;

				if(player.facingRight){
					Rigidbody2D bulletInstance1 = Instantiate(chargedWind, transform.position = new Vector3(transform.position.x, transform.position.y + bulletSpeedWindOffsetY*2f, transform.position.z), Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
					bulletInstance1.velocity = new Vector2(bulletSpeed, 0f);
					Rigidbody2D bulletInstance2 = Instantiate(chargedWind,transform.position = new Vector3(transform.position.x, transform.position.y - bulletSpeedWindOffsetY*4f, transform.position.z), Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
					bulletInstance2.velocity = new Vector2(bulletSpeed, 0f);
					Rigidbody2D bulletInstance3 = Instantiate(chargedWind, transform.position = new Vector3(transform.position.x, transform.position.y + bulletSpeedWindOffsetY*2f, transform.position.z), Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
					bulletInstance3.velocity = new Vector2(bulletSpeed, 0f);
				}
				else{
					Rigidbody2D bulletInstance1 = Instantiate(chargedWind, transform.position = new Vector3(transform.position.x, transform.position.y + bulletSpeedWindOffsetY*2f, transform.position.z), Quaternion.Euler(new Vector3(0,180,0))) as Rigidbody2D;
					bulletInstance1.velocity = new Vector2(-1*bulletSpeed, 0f);
					Rigidbody2D bulletInstance2 = Instantiate(chargedWind, transform.position = new Vector3(transform.position.x, transform.position.y  - bulletSpeedWindOffsetY*4f, transform.position.z), Quaternion.Euler(new Vector3(0,180,0))) as Rigidbody2D;
					bulletInstance2.velocity = new Vector2(-1*bulletSpeed, 0f);
					Rigidbody2D bulletInstance3 = Instantiate(chargedWind, transform.position = new Vector3(transform.position.x, transform.position.y + bulletSpeedWindOffsetY*2f, transform.position.z), Quaternion.Euler(new Vector3(0,180,0))) as Rigidbody2D;
					bulletInstance3.velocity = new Vector2(-1*bulletSpeed, 0f);
				}
				
				Invoke ("fireTrue", windRateOfFire);
				Invoke ("endAttack",0.2f);

				
				Charged = false;
				chargeDelayTime = chargeReset;
			}
			//In the air
			if(Input.GetButtonDown("Fire1") && fireEnabled && !player.attacking && !player.abletojump){
				anim.SetTrigger("RangeAttack");
				anim.SetBool("isAttacking",true);
				
				fireEnabled = false;
				player.attacking = true;
				
				if(player.facingRight){
					Rigidbody2D bulletInstance1 = Instantiate(Wind, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
					bulletInstance1.velocity = new Vector2(bulletSpeed, 0f);
					Rigidbody2D bulletInstance2 = Instantiate(Wind, transform.position, Quaternion.Euler(new Vector3(0,0,15))) as Rigidbody2D;
					bulletInstance2.velocity = new Vector2(bulletSpeed, bulletSpeedWindY);
					Rigidbody2D bulletInstance3 = Instantiate(Wind, transform.position, Quaternion.Euler(new Vector3(0,0,-15))) as Rigidbody2D;
					bulletInstance3.velocity = new Vector2(bulletSpeed, -1*bulletSpeedWindY);
				}
				else{
					Rigidbody2D bulletInstance1 = Instantiate(Wind, transform.position, Quaternion.Euler(new Vector3(0,180,0))) as Rigidbody2D;
					bulletInstance1.velocity = new Vector2(-1*bulletSpeed, 0f);
					Rigidbody2D bulletInstance2 = Instantiate(Wind, transform.position, Quaternion.Euler(new Vector3(0,180,15))) as Rigidbody2D;
					bulletInstance2.velocity = new Vector2(-1*bulletSpeed, bulletSpeedWindY);
					Rigidbody2D bulletInstance3 = Instantiate(Wind, transform.position, Quaternion.Euler(new Vector3(0,180,-15))) as Rigidbody2D;
					bulletInstance3.velocity = new Vector2(-1*bulletSpeed, -1*bulletSpeedWindY);
				}
				
				Invoke ("fireTrue", windRateOfFire);
				Invoke ("endAttack",0.2f);

			}
			//Charged (Wind)
			if(Input.GetButtonUp ("Fire1") && Charged && !player.attacking && !player.abletojump){
				anim.SetTrigger("RangeAttack");
				anim.SetBool("isAttacking",true);

				fireEnabled = false;
				player.attacking = true;

				if(player.facingRight){
					Rigidbody2D bulletInstance1 = Instantiate(chargedWind, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
					bulletInstance1.velocity = new Vector2(bulletSpeed, 0f);
					Rigidbody2D bulletInstance2 = Instantiate(chargedWind, transform.position, Quaternion.Euler(new Vector3(0,0,15))) as Rigidbody2D;
					bulletInstance2.velocity = new Vector2(bulletSpeed, bulletSpeedWindY);
					Rigidbody2D bulletInstance3 = Instantiate(chargedWind, transform.position, Quaternion.Euler(new Vector3(0,0,-15))) as Rigidbody2D;
					bulletInstance3.velocity = new Vector2(bulletSpeed, -1*bulletSpeedWindY);
				}
				else{
					Rigidbody2D bulletInstance1 = Instantiate(chargedWind, transform.position, Quaternion.Euler(new Vector3(0,180,0))) as Rigidbody2D;
					bulletInstance1.velocity = new Vector2(-1*bulletSpeed, 0f);
					Rigidbody2D bulletInstance2 = Instantiate(chargedWind, transform.position, Quaternion.Euler(new Vector3(0,180,15))) as Rigidbody2D;
					bulletInstance2.velocity = new Vector2(-1*bulletSpeed, bulletSpeedWindY);
					Rigidbody2D bulletInstance3 = Instantiate(chargedWind, transform.position, Quaternion.Euler(new Vector3(0,180,-15))) as Rigidbody2D;
					bulletInstance3.velocity = new Vector2(-1*bulletSpeed, -1*bulletSpeedWindY);
				}
				
				Invoke ("fireTrue", windRateOfFire);
				Invoke ("endAttack",0.2f);

				
				Charged = false;
				chargeDelayTime = chargeReset;
			}
		}
	}
	void fire(GameObject bullet){
		anim.SetTrigger("RangeAttack");
		anim.SetBool("isAttacking",true);
		
		fireEnabled = false;
		player.attacking = true;
		
		if(player.facingRight){
			Rigidbody2D bulletInstance = Instantiate(chargedBullet, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2(bulletSpeed, 0);
		}
		else{
			Rigidbody2D bulletInstance = Instantiate(chargedBullet, transform.position, Quaternion.Euler(new Vector3(0,180,0))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2(-1*bulletSpeed, 0);
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
