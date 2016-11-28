using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public Durability healthamount;
	Image healthbar;

	// Use this for initialization
	void Start () {
		healthbar = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		healthbar.fillAmount = healthamount.HP / (float)(healthamount.maxHP);
	}
}
