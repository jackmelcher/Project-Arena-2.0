using UnityEngine;
using System.Collections;

public class BulletOscillate : MonoBehaviour {

	private Rigidbody2D body2D;

	public float SpeedY;
	public float theta = 0f;
	public float oscillate;
	public float period = 12;

	// Use this for initialization
	void Start () {
		body2D = GetComponent<Rigidbody2D> ();
		SpeedY = body2D.velocity.y;
	}
	
	// Update is called once per frame
	void Update () {
		theta += Mathf.PI / period;
		oscillate = Mathf.Cos (theta);
		body2D.velocity = new Vector2(body2D.velocity.x, oscillate*SpeedY);
			
	}
}
