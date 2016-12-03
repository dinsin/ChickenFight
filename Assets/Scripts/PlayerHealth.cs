using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public Durability healthamount;
	Image healthbar;
	int segmentCount = 10;

	// Use this for initialization
	void Start () {
		healthbar = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		float amount = healthamount.HP / healthamount.maxHP;
		if (amount < 0.1f && amount > 0.0f)
		{
			//If less than 1 bar of HP, show 1 bar of HP
			amount = Mathf.Max(amount, 0.1f);
		} else
		{
			//Else, segment HP
			amount = (int)((amount) * segmentCount) / (float)segmentCount;
		}
		healthbar.fillAmount = amount;
	}
}
