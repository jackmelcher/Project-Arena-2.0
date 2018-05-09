using UnityEngine;
using System.Collections;

public class BeamShot : MonoBehaviour {

	public KeyCode hotkey;
	//Bullet Parameters
	public float bulletSpeed = 20f;
	public Rigidbody2D bullet;
	
	//Firing Parameters
	public float rateOfFire = 0.2f;
	public float delay = 0.3f;
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
			if(Input.GetKey(hotkey) && !player.attacking){
				standardFire(bullet);
			}
		}
	}
	
	void standardFire(Rigidbody2D bullet){
		anim.SetTrigger("RangeAttack");
		anim.SetBool("isAttacking",true);
		int bulletSpeedY = (int) Random.Range (-4f, 4f);
		float bulletSpeedX = Mathf.Sqrt(Mathf.Pow(bulletSpeed,2) - Mathf.Pow(bulletSpeedY,2));


		player.attacking = true;
		fireEnabled = false;

		if(player.facingRight){
			Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2(bulletSpeedX, bulletSpeedY);
			//bullet's orientation
			if(bulletSpeedX >=0 && bulletSpeedY >= 0)
				bulletInstance.transform.root.eulerAngles = new Vector3(0f,0f,Mathf.Atan(bulletSpeedY/bulletSpeedX)*180/Mathf.PI);
			else if(bulletSpeedX <=0 && bulletSpeedY >= 0)
				bulletInstance.transform.root.eulerAngles = new Vector3(0f,0f,180+Mathf.Atan(bulletSpeedY/bulletSpeedX)*180/Mathf.PI);
			else if(bulletSpeedX <=0 && bulletSpeedY <= 0)
				bulletInstance.transform.root.eulerAngles = new Vector3(0f,0f,180+Mathf.Atan(bulletSpeedY/bulletSpeedX)*180/Mathf.PI);
			else if(bulletSpeedX >=0 && bulletSpeedY <= 0)
				bulletInstance.transform.root.eulerAngles = new Vector3(0f,0f,360+Mathf.Atan(bulletSpeedY/bulletSpeedX)*180/Mathf.PI);
		}
		else{
			bulletSpeedX = -1*bulletSpeedX;

			Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0,180,0))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2(bulletSpeedX, bulletSpeedY);
			//bullet's orientation
			bulletInstance.transform.localScale = new Vector3(1,-1,1);
			if(bulletSpeedX >=0 && bulletSpeedY >= 0)
				bulletInstance.transform.root.eulerAngles = new Vector3(0f,0f,Mathf.Atan(bulletSpeedY/bulletSpeedX)*180/Mathf.PI);
			else if(bulletSpeedX <=0 && bulletSpeedY >= 0)
				bulletInstance.transform.root.eulerAngles = new Vector3(0f,0f,180+Mathf.Atan(bulletSpeedY/bulletSpeedX)*180/Mathf.PI);
			else if(bulletSpeedX <=0 && bulletSpeedY <= 0)
				bulletInstance.transform.root.eulerAngles = new Vector3(0f,0f,180+Mathf.Atan(bulletSpeedY/bulletSpeedX)*180/Mathf.PI);
			else if(bulletSpeedX >=0 && bulletSpeedY <= 0)
				bulletInstance.transform.root.eulerAngles = new Vector3(0f,0f,360+Mathf.Atan(bulletSpeedY/bulletSpeedX)*180/Mathf.PI);
		}
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
