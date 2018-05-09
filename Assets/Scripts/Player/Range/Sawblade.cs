using UnityEngine;
using System.Collections;

public class Sawblade : MonoBehaviour {

	public float rotationSpeed = -10f;
	public float reverseAccel = -1f;
	public float reverseSpeed = -20f;
	public bool right = false;
	public bool decelerate = true;

	//For Turret fire setting
	[HideInInspector]
	public float bulletSpeed = 0f;
	[HideInInspector]
	public float bulletHeight = 0f;
	[HideInInspector]
	public float bulletWidth = 0f;
	[HideInInspector]
	public float bulletMagnitude = 0f;

	private Rigidbody2D body2D;
	private Transform player;

	// Use this for initialization
	void Start () {
		body2D = transform.root.GetComponent<Rigidbody2D>();
		if(body2D.velocity.x > 0f){
			right = true;
		}
		else{
			right = false;
		}

		player = GameObject.Find("Player Character").GetComponent<Transform>();

		bulletSpeed = Mathf.Abs(body2D.velocity.x);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0f,0f,transform.rotation.z + rotationSpeed);

		if(body2D.velocity.x > reverseSpeed && right && decelerate)
			body2D.velocity = new Vector2 (body2D.velocity.x + reverseAccel, body2D.velocity.y);
		if(body2D.velocity.x < -1*reverseSpeed && !right && decelerate)
			body2D.velocity = new Vector2 (body2D.velocity.x - reverseAccel, body2D.velocity.y);
		if(body2D.velocity.x < 0 && right){
			decelerate = false;
		}
		if(body2D.velocity.x > 0 && !right){
			decelerate = false;
		}
	

		if(!decelerate){
			bulletWidth = player.position.x - transform.position.x;
			bulletHeight = player.position.y - transform.position.y;
			bulletMagnitude = Mathf.Sqrt(Mathf.Pow(bulletWidth,2)+Mathf.Pow(bulletHeight,2));
			body2D.velocity = new Vector2(bulletSpeed*bulletWidth/bulletMagnitude, bulletSpeed*bulletHeight/bulletMagnitude);
		}




	}

	void OnTriggerEnter2D (Collider2D coll2D){
		if(coll2D.tag == "Player"){
			Destroy(this.gameObject);
		}
	}
}
