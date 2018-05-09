using UnityEngine;
using System.Collections;

public class ChargeShot : MonoBehaviour {

	public KeyCode hotkey;
	//Bullet Parameters
	public float bulletSpeedX = 20f;
	public Rigidbody2D bullet;
	public Rigidbody2D bulletCharged1;
	public Rigidbody2D bulletCharged2;

	//Firing Parameters
	public float delay = 0.2f;
	public float rateOfFire = 0.3f;
	public bool fireEnabled = true;

	//Charge Parameters
	public float chargeDelayTime = 3.0f;
	public float chargeReset = 3.0f;
	public bool Charged1 = false;
	public bool Charged2 = false;

	//References
	private PlayerControl2D player;
	private Animator anim;

	// Use this for initialization
	void Start () {
		player = transform.root.GetComponent<PlayerControl2D>();
		anim = transform.root.gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
		if(fireEnabled){
			//Charging Mechanism
			if(Input.GetKey (hotkey) && chargeDelayTime > 0){
				chargeDelayTime = chargeDelayTime - Time.deltaTime;
			}
			//Firing Mechanism
			//Standard Shot
			if(Input.GetKeyDown(hotkey) && !player.attacking){
				standardFire(bullet);
			}
			//Charged Stage 1
			if(Input.GetKeyUp(hotkey) && chargeDelayTime <= chargeReset/2 && chargeDelayTime > 0 && !player.attacking){
				standardFire(bulletCharged1);
			}
			//Charge Stage 2
			if(Input.GetKeyUp(hotkey) && chargeDelayTime <= 0 && !player.attacking){
				standardFire(bulletCharged1);
			}
			//Reset Charge time
			if(!Input.GetKey(hotkey)){
				chargeDelayTime = chargeReset;
			}
		}
	}

	void standardFire(Rigidbody2D bullet){
		anim.SetTrigger("RangeAttack");
		anim.SetBool("isAttacking",true);
		
		player.attacking = true;
		fireEnabled = false;

		if(player.facingRight){
			Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2(bulletSpeedX, 0);
		}
		else{
			Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0,180,0))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2(-1*bulletSpeedX, 0);
		}

		Invoke ("endAttack",delay);
		Invoke ("fireTrue", rateOfFire);
	}
	void endAttack(){
		anim.SetBool("isAttacking", false);
		player.attacking = false;
	}
	void fireTrue (){
		fireEnabled = true;
	}
}
