using UnityEngine;
using System.Collections;

public class PlayerFXScript2 : MonoBehaviour {

	public float animDelay = 2f;
	ChargeShot script;
	Animator anim;
	
	void Start(){
		anim = transform.gameObject.GetComponent<Animator>();
		script = transform.root.Find("Long Ranged Projectile").GetComponent<ChargeShot>();
	}
	
	void Update () {
		if(script.chargeDelayTime <= 0)
			anim.SetBool("Charged",true);
		else if(script.chargeDelayTime < script.chargeReset/2)
			anim.SetBool("isCharging",true);
		else{
			anim.SetBool("Charged",false);
			anim.SetBool("isCharging",false);
		}
	}
}
