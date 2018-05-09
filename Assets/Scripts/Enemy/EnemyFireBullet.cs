using UnityEngine;
using System.Collections;

public class EnemyFireBullet : MonoBehaviour {

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

	public float fireRate = 2f;
	public float count = 0f;
	public float gravityScale = 0f;
	public bool enable = false;

	//Firing Modes
	public bool fireEnable = false;
	//Fixed Direction
	public bool fireLeft = false;
	public bool fireRight = false;
	public bool fireUp = false;
	public bool fireDown = false;
	//Tracking
	public bool turret = false;
	//Multiple shots in succession
	public bool rapidFire = false;
	public float bulletCounter = 0f;
	public float bulletCountMax = 10f;
	//Multiple shots in a spread

	private EnemyControllerVer2 enemy;
	private Transform player;

	void Start (){
		enemy = transform.root.GetComponent<EnemyControllerVer2>();
		player = GameObject.Find("Player Character").GetComponent<Transform>();
	}

	void Update () {
		//Acquire player position and adjust angle of fire for turret mode
		if(turret && player && bulletCounter == 0f){
			bulletWidth = player.position.x - transform.position.x;
			bulletHeight = player.position.y - transform.position.y;
			bulletMagnitude = Mathf.Sqrt(Mathf.Pow(bulletWidth,2)+Mathf.Pow(bulletHeight,2));

			if(bulletWidth >=0 && bulletHeight >= 0)
				transform.root.eulerAngles = new Vector3(0f,0f,Mathf.Atan(bulletHeight/bulletWidth)*180/Mathf.PI);
			else if(bulletWidth <=0 && bulletHeight >= 0)
				transform.root.eulerAngles = new Vector3(0f,0f,180+Mathf.Atan(bulletHeight/bulletWidth)*180/Mathf.PI);
			else if(bulletWidth <=0 && bulletHeight <= 0)
				transform.root.eulerAngles = new Vector3(0f,0f,180+Mathf.Atan(bulletHeight/bulletWidth)*180/Mathf.PI);
			else if(bulletWidth >=0 && bulletHeight <= 0)
				transform.root.eulerAngles = new Vector3(0f,0f,360+Mathf.Atan(bulletHeight/bulletWidth)*180/Mathf.PI);
		}

		if(enable){
			count += Time.deltaTime;
			if (count >= fireRate) {
				if(rapidFire){
					InvokeRepeating ("Fire", 0f, 0.2f);
				}
				else{Invoke ("Fire", 0f);}
				count = 0;
			}
		}
		else{count = 0;}
	}

	void Fire (){
		if(fireEnable){
			if(enemy.facingRight){
				Rigidbody2D bulletInstance = Instantiate(enemyBullet, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(bulletSpeed, bulletSpeedY);
				bulletInstance.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
			}
			else{
				Rigidbody2D bulletInstance = Instantiate(enemyBullet, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(-1*bulletSpeed, bulletSpeedY);
				bulletInstance.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
			}
		}
		if (turret) {
			Rigidbody2D bulletInstance = Instantiate(enemyBullet, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2(bulletSpeed*bulletWidth/bulletMagnitude, bulletSpeed*bulletHeight/bulletMagnitude);
		}
		//Rapid Fire Setting
		if(rapidFire){
			count = 0;
			bulletCounter++;
			if(bulletCounter > bulletCountMax) {
				bulletCounter = 0;
				CancelInvoke("Fire");
			}
		}

	}

}
