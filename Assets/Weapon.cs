using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public int totalDamage;

	public GameObject barrel;
	public GameObject body;
	public GameObject bullet;
	public GameObject magazine;
	private Weapon_Barrel barrel_s;
	private Weapon_Body body_s;
	private Weapon_Bullet bullet_s;
	private Weapon_Magazine magazine_s;

	//Barrel Parameters
	public string pathing;
	public int barrel_count;

	//Body Parameters
	public string mode;
	public float speed;
	public int drop;

	//Bullet Parameters
	public string effect;

	//Magazine Parameters
	public float capacity;

	//Firing Parameters
	public float rateOfFire = 0.2f;
	public float delay = 0.3f;
	public bool fireEnabled = true;
	public bool reloading = false;
	public Vector3 mousePosition;

	//References
	private PlayerControl2D player;
	private Animator anim;

	// Use this for initialization
	void Start () {
		barrel_s = barrel.GetComponent<Weapon_Barrel>();
		body_s = body.GetComponent<Weapon_Body>();
		bullet_s = bullet.GetComponent<Weapon_Bullet>();
		magazine_s = magazine.GetComponent<Weapon_Magazine>();

		pathing = barrel_s.pathing;
		barrel_count = barrel_s.barrel_count;

		mode = body_s.mode;
		speed = body_s.speed;
		drop = body_s.drop;

		effect = bullet_s.effect;

		capacity = magazine_s.capacity;

		totalDamage = barrel_s.damage + body_s.damage +bullet_s.damage +magazine_s.damage;

		player = transform.root.GetComponent<PlayerControl2D>();
		anim = transform.root.gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		mousePosition = Input.mousePosition;
		mousePosition.z = 0f;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		float width = Mathf.Abs (mousePosition.x - transform.position.x);
		float height = mousePosition.y - transform.position.y;
		float magnitude = Mathf.Sqrt(Mathf.Pow(width,2)+Mathf.Pow(height,2));
		transform.localPosition = new Vector3 (width/magnitude,0.5f+height/magnitude,transform.position.z);

		if(fireEnabled && !reloading){
			//Firing Mechanism
			if(Input.GetButtonDown("Fire1") && !player.attacking){

				standardFire(bullet);
				capacity--;
			}
		}
		if(Input.GetKeyDown(KeyCode.R) && capacity < magazine_s.capacity){
			capacity = 0;
		}
		if(capacity <= 0){
			reloading = true;
		}
		if(reloading){
			capacity += Time.deltaTime;
			if(capacity >= magazine_s.capacity){
				capacity = magazine_s.capacity;
				reloading = false;
			}
		}
	}

	void standardFire(GameObject projectile){
		anim.SetTrigger("RangeAttack");
		anim.SetBool("isAttacking",true);
				
		player.attacking = true;
		fireEnabled = false;

		mousePosition = Input.mousePosition;
		mousePosition.z = 0f;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		float bulletSpeedX = mousePosition.x - transform.position.x;
		float bulletSpeedY = mousePosition.y - transform.position.y;
		float bulletMagnitude = Mathf.Sqrt(Mathf.Pow(bulletSpeedX,2)+Mathf.Pow(bulletSpeedY,2));

		//bullet properties
		GameObject bulletInstance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
		//bullet damage
		bulletInstance.GetComponent<Bullet_Property>().bulletDamage = totalDamage;
		//bullet's orientation
		if(bulletSpeedX >=0 && bulletSpeedY >= 0)
			bulletInstance.transform.root.eulerAngles = new Vector3(0f,0f,Mathf.Atan(bulletSpeedY/bulletSpeedX)*180/Mathf.PI);
		else if(bulletSpeedX <=0 && bulletSpeedY >= 0)
			bulletInstance.transform.root.eulerAngles = new Vector3(0f,0f,180+Mathf.Atan(bulletSpeedY/bulletSpeedX)*180/Mathf.PI);
		else if(bulletSpeedX <=0 && bulletSpeedY <= 0)
			bulletInstance.transform.root.eulerAngles = new Vector3(0f,0f,180+Mathf.Atan(bulletSpeedY/bulletSpeedX)*180/Mathf.PI);
		else if(bulletSpeedX >=0 && bulletSpeedY <= 0)
			bulletInstance.transform.root.eulerAngles = new Vector3(0f,0f,360+Mathf.Atan(bulletSpeedY/bulletSpeedX)*180/Mathf.PI);
		//bullet speed
		bulletInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(speed*bulletSpeedX/bulletMagnitude, speed*bulletSpeedY/bulletMagnitude);

		//Time between shots.
		Invoke ("fireTrue", rateOfFire);
		//Time for when you stop attacking and allow for other attacks to activate
		Invoke ("endAttack",delay);
	}

	void fireTrue (){
		fireEnabled = true;
	}
	void endAttack(){
		anim.SetBool("isAttacking", false);
		player.attacking = false;
	}
}
