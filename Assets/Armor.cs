using UnityEngine;
using System.Collections;

public class Armor : MonoBehaviour {

	public int maxArmor;
	public GameObject helmet;
	public GameObject body;
	public GameObject boots;

	// Use this for initialization
	void Awake () {
		int helmet_armor = helmet.GetComponent<Armor_Helmet>().armor;
		int body_armor = body.GetComponent<Armor_Body>().armor;
		int boot_armor = boots.GetComponent<Armor_Boots>().armor;
		maxArmor = helmet_armor + body_armor + boot_armor;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
