using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

	Image myImage;
	private PlayerHealthManager playerHealth;
	public float Current = 0f;

	void Awake (){
		myImage = GetComponent<Image>();
	}
	
	void Start () {
		playerHealth = GameObject.Find("Player Character").GetComponent<PlayerHealthManager>();
	}

	void Update () {
		Current = playerHealth.health/2;
		myImage.fillAmount = Current/10f;
	}
}
