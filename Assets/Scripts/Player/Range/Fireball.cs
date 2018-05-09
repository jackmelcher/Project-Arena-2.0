using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {
	public float rotationSpeed = -10f;
	public float bounceSpeed = 5f;
	public float maxFallSpeed = 10f;
	private Rigidbody2D body;

	// Use this for initialization
	void Start () {
		body = transform.root.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0f,0f,rotationSpeed);

		if (transform.root.position.y <= -maxFallSpeed) {
			body.velocity = new Vector2 (body.velocity.x, -maxFallSpeed);
		}
	}

	void OnTriggerEnter2D (Collider2D coll){
		if (coll.gameObject.tag == "Block") {
			if(coll.transform.position.y < transform.position.y){
				body.velocity = new Vector2 (body.velocity.x,bounceSpeed);
			}
		}
	}

}
