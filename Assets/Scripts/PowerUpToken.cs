using UnityEngine;
using System.Collections;

public class PowerUpToken : MonoBehaviour {

	public enum PowerType { ExtraEggs, SuperShot, EggSupply, Health, SuperSpeed };

	public PowerType powertype;
	public GameObject powerupSound;
	float duration;

	// Use this for initialization
	void Start () {
		duration = 10.0f + Random.value * 5.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (duration > 0)
		{
			duration -= Time.deltaTime;
		} else
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D cd)
	{
		PowerUpCollector collector = cd.GetComponent<PowerUpCollector>();
		if (collector)
		{
			if (powertype == PowerType.ExtraEggs)
			{
				collector.GetExtraEggThrows();
			}
			else if (powertype == PowerType.SuperShot)
			{
				collector.GetSuperShot();
			}
			else if (powertype == PowerType.EggSupply)
			{
				collector.GetEggSupply();
			}
			else if (powertype == PowerType.SuperSpeed)
			{
				collector.GetExtremeSpeed();
			}
			else
			{
				collector.GetHPBoost();
			}
			Instantiate(powerupSound);
			Destroy(gameObject);
		}
	}
}
