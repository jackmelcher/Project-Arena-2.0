using UnityEngine;
using System.Collections;

public class DestroyBullet : MonoBehaviour {
	public float bulletLifetime = 1f;
	public bool enemyPierce = false;
	public bool wallPierce = false;
	
	public int bulletDamage = 1;

	void Start(){
		Invoke ("destroy", bulletLifetime);
	}

	void OnTriggerEnter2D (Collider2D coll2D){
		//Bullet Settings
		if((coll2D.gameObject.tag == "Enemy") && coll2D.GetComponent<EnemyHealthver2>() != null){
			if(!coll2D.GetComponent<EnemyHealthver2>().invincible){
				coll2D.GetComponent<EnemyHealthver2>().health -= bulletDamage;

				if(coll2D.gameObject.tag == "Enemy" && !enemyPierce)
					Destroy (gameObject);
				if(coll2D.gameObject.tag == "Enemy")
					coll2D.GetComponent<EnemyHealthver2>().hit = true;
			}
		}

		//Destroy Settings
		if((coll2D.gameObject.tag == "Block" || coll2D.gameObject.tag == "Floor" || coll2D.gameObject.tag == "Slant"|| coll2D.gameObject.tag == "Wall") && !wallPierce)
			Destroy (gameObject);

	}

	void OnTriggerExit2D (Collider2D coll2D){
		if(coll2D.gameObject.tag == "MainCamera"){
			Destroy (gameObject);
		}
	}

	void destroy (){
		Destroy (gameObject);
	}
}
