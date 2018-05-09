using UnityEngine;
using System.Collections;

public class PlayerFXScript : MonoBehaviour {

	FireBullet script;
	Animator anim;
	
	void Start(){
		anim = transform.gameObject.GetComponent<Animator>();
		script = transform.root.Find("Weapon Spawn").GetComponent<FireBullet>();
	}

	void Update () {
		if(script.Charged)
			anim.SetBool("Charged",true);
		else if(script.chargeDelayTime < 1.8f)
			anim.SetBool("isCharging",true);
		else{
			anim.SetBool("Charged",false);
			anim.SetBool("isCharging",false);
		}
	}
}
