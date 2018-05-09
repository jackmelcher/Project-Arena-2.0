using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponBar : MonoBehaviour {

	Image myImage;
	private Weapon weapon;
	public float Current;
	
	void Awake (){
		myImage = GetComponent<Image>();
	}
	
	void Start () {
		weapon = GameObject.Find("Player Character").GetComponentInChildren<Weapon>();
	}
	
	void Update () {
		Current = weapon.capacity;
		myImage.fillAmount = Current/10f;
	}
}
