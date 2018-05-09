using UnityEngine;
using System.Collections;

public class BlastShot : MonoBehaviour {

	public KeyCode hotkey;
	//Bullet Parameters
	public float bulletSpeedX = 20f;
	public Rigidbody2D bullet;
	public Rigidbody2D bulletUpgrade;

	//Firing Parameters
	public float delay = 0.2f;
	public float rateOfFire = 0.3f;
	public bool fireEnabled = true;
	
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
			//Firing Mechanism
			if(Input.GetKeyDown(hotkey) && !player.attacking){
				standardFire(bullet);
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
