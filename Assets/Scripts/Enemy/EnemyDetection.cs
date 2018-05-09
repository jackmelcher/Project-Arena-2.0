using UnityEngine;
using System.Collections;

public class EnemyDetection : MonoBehaviour {

	float width = 0f;
	float height = 0f;
	private Camera cam;
	private BoxCollider2D box;

	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera> ();
		box = GetComponent<BoxCollider2D> ();

	}
	
	// Update is called once per frame
	void Update () {
		height = 2f*cam.orthographicSize;
		width = height*cam.aspect;
		box.size = new Vector2(width,height);
	}

}
