using UnityEngine;
using System.Collections;

public class ChargeBulletDestroy : MonoBehaviour {

	void Start()
	{
		Invoke ("destroy", 2.0f);
	}
	
	void OnTriggerEnter2D (Collider2D destroyBullet)
	{
		if(destroyBullet.gameObject.tag == "Floor" || destroyBullet.gameObject.tag == "Slant")
			Destroy (gameObject);
	}
	
	void destroy ()
	{
		Destroy (gameObject);
	}
}
