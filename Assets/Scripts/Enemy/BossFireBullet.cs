using UnityEngine;
using System.Collections;

public class BossFireBullet : MonoBehaviour {
	
	public Rigidbody2D enemyBullet;
	//General Fire Settings
	public float bulletSpeed = 30f;
	public float bulletSpeedY = 0f;
	//For Turret fire setting
	[HideInInspector]
	public float bulletHeight = 0f;
	[HideInInspector]
	public float bulletWidth = 0f;
	[HideInInspector]
	public float bulletMagnitude = 0f;

	public float angle = 0f;
	public float fireRate = 2f;
	public float count = 0f;
	public float gravityScale = 0f;

	public bool turret = false;

	public bool rapidFire = false;
	public float bulletCounter = 0f;
	public float bulletCountMax = 10f;

	public bool enable = false;
	public bool fireEnable = false;
	
	private BossController boss;
	private Transform player;
	
	void Start (){
		boss = transform.root.GetComponent<BossController>();

		player = GameObject.Find("Player Character").GetComponent<Transform>();
	}
	
	void Update () {
		//Acquire player position and adjust angle of fire for turret mode
		if(turret && player && bulletCounter == 0f){
			bulletWidth = player.position.x - transform.position.x;
			bulletHeight = player.position.y - transform.position.y;
			bulletMagnitude = Mathf.Sqrt(Mathf.Pow(bulletWidth,2)+Mathf.Pow(bulletHeight,2));
		}

	}
	
	void Fire (){
		//Fixed mode
		if(boss.facingRight && !turret){
			Rigidbody2D bulletInstance = Instantiate(enemyBullet, transform.position, Quaternion.Euler(new Vector3(0,180,0))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2(bulletSpeed, bulletSpeedY);
			bulletInstance.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
		}
		else if(!boss.facingRight && !turret){
			Rigidbody2D bulletInstance = Instantiate(enemyBullet, transform.position, Quaternion.Euler(new Vector3(0,0,angle))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2(-1*bulletSpeed, bulletSpeedY);
			bulletInstance.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
		}
		//Turret mode
		if (turret) {
			Rigidbody2D bulletInstance = Instantiate(enemyBullet, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2(bulletSpeed*bulletWidth/bulletMagnitude, bulletSpeed*bulletHeight/bulletMagnitude);

		}
		//Rapid Fire Setting
		if(rapidFire){
			bulletCounter++;
			if(bulletCounter > bulletCountMax) {
				bulletCounter = 0;
				CancelInvoke("Fire");
			}
		}
	}
}
