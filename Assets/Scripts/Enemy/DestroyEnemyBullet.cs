using UnityEngine;
using System.Collections;

public class DestroyEnemyBullet : MonoBehaviour {

	public float bulletLifetime = 1f;
	public int bulletDamage = 1;
	public bool playerPierce = false;
	public bool wallPierce = false;
	public bool explosive = false;

	public Rigidbody2D explosion;

	void Start(){
		Invoke ("destroy", bulletLifetime);
	}

	void OnTriggerEnter2D (Collider2D coll2D){
		if(coll2D.gameObject.tag == "Block" && !wallPierce){
			if(explosive){
				Rigidbody2D bulletInstance = Instantiate(explosion, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(0f, 0f);
			}
			Destroy (gameObject);
		}

		if(coll2D.gameObject.tag == "Player" && coll2D.GetComponent<PlayerHealthManager>() != null){
			if(!coll2D.GetComponent<PlayerHealthManager>().invincible){
				coll2D.GetComponent<PlayerHealthManager>().health -= bulletDamage;
				coll2D.GetComponent<PlayerHealthManager>().hit = true;
				if(transform.position.x > coll2D.transform.position.x){
					coll2D.GetComponent<PlayerHealthManager>().knockbackLeft = true;
				}
				else if(transform.position.x < coll2D.transform.position.x){
					coll2D.GetComponent<PlayerHealthManager>().knockbackRight = true;
				}
			}
			if(explosive){
				Rigidbody2D bulletInstance = Instantiate(explosion, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(0f, 0f);
			}
			if(!playerPierce){
				Destroy (gameObject);
			}
		}

		if(coll2D.gameObject.name.Contains("Laser Door")){
			Destroy (gameObject);

		}
	}

	void destroy (){
		Destroy (gameObject);
	}
}
