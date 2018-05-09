using UnityEngine;
using System.Collections;

public class Boomerang : MonoBehaviour {

	private Rigidbody2D body2D;
	public float SpeedX;
	public float SpeedY;
	public float rotationSpeed = -10f;
	public float returnDistance = 2f;
	public float theta = 0f;
	public float oscillate1;
	public float oscillate2;
	public float period = 12;
	
	// Use this for initialization
	void Start () {
		body2D = GetComponent<Rigidbody2D> ();
		SpeedX = -body2D.velocity.x;
		SpeedY = body2D.velocity.y;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0f,0f,transform.rotation.z + rotationSpeed);

		if(returnDistance > 0){
			returnDistance -= Time.deltaTime;
		}
		else{
			theta += Mathf.PI / period;
			oscillate1 = Mathf.Cos (theta);
			oscillate2 = Mathf.Sin (theta);
			body2D.velocity = new Vector2(oscillate2*SpeedX, oscillate1*SpeedY);
		}

	}
}
