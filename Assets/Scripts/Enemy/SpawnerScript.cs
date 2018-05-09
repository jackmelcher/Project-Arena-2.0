using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour {

	public Rigidbody2D enemy;
	public float timer = 3.0f;
	public float timerReset = 3.0f;
	public float MaxSpawnAmount = 5.0f;

	void Update ()
	{
		if(MaxSpawnAmount > 0)
		{
			if(timer > 0)
				timer = timer - Time.deltaTime;

			if(timer <= 0)
			{
				Rigidbody2D enemyInstance = Instantiate(enemy, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
				enemyInstance.velocity = new Vector2(0, 0);
				timer = timerReset;
				MaxSpawnAmount--;
			}
		}
	}
}
