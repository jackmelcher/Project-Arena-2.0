using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public bool followPlayer = true;

	public bool cameraSpot = false;
	public float cameraTranslateSpeed = 20f;

	public GameObject player;
	public Transform spot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(followPlayer && player){
			transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, transform.position.z);
		}
		if (cameraSpot) {
			transform.position = Vector3.MoveTowards(transform.position, spot.transform.position, cameraTranslateSpeed * Time.deltaTime);
		}
	}

	void OnTriggerEnter2D (Collider2D coll2D){
		if (coll2D.gameObject.name == "CameraSpot") {
			followPlayer = false;
			cameraSpot = true;
			spot = coll2D.transform;
		}
	}
}
