/* ChickenFight
 * Author: Kevin Zeng, Dinesh Singh, Jon Wu */
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
		if (amount > 0.1f)
		{
			//Else, segment HP
			amount = (int)((amount) * segmentCount) / (float)segmentCount;
		}
		healthbar.fillAmount = Mathf.Lerp(healthbar.fillAmount, amount, 0.2f);
	}
}
