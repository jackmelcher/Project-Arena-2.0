using UnityEngine;
using System.Collections;

public class Icicle : MonoBehaviour {

	public float rotationSpeed = -10f;
	public float rotate1 = 300f;
	public float rotate2 = 320f;
	public bool rotate = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.localEulerAngles.z >= rotate1 && transform.localEulerAngles.z <= rotate2 )
			rotate = false;
		if(rotate)
			transform.Rotate(0f,0f,transform.rotation.z + rotationSpeed);
	}
}
