using UnityEngine;
using System.Collections;

public class ElementSystem : MonoBehaviour {

	public bool fire = false;
	public bool ice = false;
	public bool lightning = false;
	public bool wind = false;

	public bool fireunlock = false;
	public bool iceunlock = false;
	public bool lightningunlock = false;
	public bool windunlock = false;

	public bool switchEnabled = true;
	
	// Update is called once per frame
	void Update () {
		if(switchEnabled){
			if(Input.GetKeyDown(KeyCode.Alpha1) && !fire && fireunlock){
				fire = true;
				ice = false;
				lightning = false;
				wind = false;

			}
			else if(Input.GetKeyDown(KeyCode.Alpha1) && fire && fireunlock){
				fire = false;
				ice = false;
				lightning = false;
				wind = false;
			}
			if(Input.GetKeyDown(KeyCode.Alpha2) && !ice && iceunlock){
				fire = false;
				ice = true;
				lightning = false;
				wind = false;
			}
			else if(Input.GetKeyDown(KeyCode.Alpha2) && ice && iceunlock){
				fire = false;
				ice = false;
				lightning = false;
				wind = false;
			}
			if(Input.GetKeyDown(KeyCode.Alpha3) && !lightning && lightningunlock){
				fire = false;
				ice = false;
				lightning = true;
				wind = false;
			}
			else if(Input.GetKeyDown(KeyCode.Alpha3) && lightning && lightningunlock){
				fire = false;
				ice = false;
				lightning = false;
				wind = false;
			}
			if(Input.GetKeyDown(KeyCode.Alpha4) && !wind && windunlock){
				fire = false;
				ice = false;
				lightning = false;
				wind = true;
			}
			else if(Input.GetKeyDown(KeyCode.Alpha4) && wind && windunlock){
				fire = false;
				ice = false;
				lightning = false;
				wind = false;
			}
		}
	}
}
