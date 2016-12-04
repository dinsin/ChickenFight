using UnityEngine;
using System.Collections;

public class PowerUpCollector : MonoBehaviour {

	public int mExtraEggThrows = 0;
	public float gravityChange = 1;
	public float attackChange = 1;
	public float sizeChange = 1;
	public float speedChange = 1;
	public float shotCost = 1;
	public float moveChange = 1;
	float extraEggDuration = 0;
	float superShotDuration = 0;
	float extremeSpeedDuration = 0;

	int powerLevel = 0;
	int shotLevel = 0;

	PlayerController pc;
	Durability dr;

	void Start ()
	{
		pc = GetComponent<PlayerController>();
		dr = GetComponent<Durability>();
	}

	// Update is called once per frame
	void Update () {
		if (extraEggDuration > 0)
		{
			extraEggDuration -= Time.deltaTime;
			if (extraEggDuration <= 0)
			{
				shotLevel = 0;
				mExtraEggThrows = 0;
				if (superShotDuration <= 0)
					shotCost = 3;
				else
					shotCost = 1;
			}
		}
		if (superShotDuration > 0)
		{
			superShotDuration -= Time.deltaTime;
			if (superShotDuration <= 0)
			{
				powerLevel = 0;
				sizeChange = 1;
				speedChange = 1;
				attackChange = 1;
				gravityChange = 1;
				if (extraEggDuration <= 0)
					shotCost = 2;
				else
					shotCost = 1;
			}
		}
		if (extremeSpeedDuration > 0)
		{
			extremeSpeedDuration -= Time.deltaTime;
			if (extremeSpeedDuration <= 0)
			{
				moveChange = 1;
				transform.localScale = Vector3.one;
			}
		}
	}

	public void GetExtraEggThrows()
	{
		shotLevel = Mathf.Min(shotLevel + 1, 5);
		extraEggDuration = 15.0f;
		if (superShotDuration <= 0)
			shotCost = 2;
		mExtraEggThrows = 1 * shotLevel;
		attackChange = Mathf.Pow(0.8f, shotLevel) * Mathf.Pow(1.2f, powerLevel);
	}

	public void GetSuperShot()
	{
		powerLevel = Mathf.Min(powerLevel + 1, 3);
		superShotDuration = 10.0f;
		shotCost = 3;
		gravityChange = Mathf.Pow(0.3f, powerLevel);
		sizeChange = Mathf.Pow(1.3f, powerLevel);
		speedChange = Mathf.Pow(0.9f, powerLevel);
		attackChange = Mathf.Pow(0.8f, shotLevel) * Mathf.Pow(1.2f, powerLevel);
	}

	public void GetExtremeSpeed()
	{
		moveChange = 1.3f;
		transform.localScale = new Vector3(0.8f, 0.8f, 1);
		extremeSpeedDuration = 10.0f;
	}

	public void GetEggSupply()
	{
		pc.eggCount = pc.maxEggCount;
	}

	public void GetHPBoost()
	{
		dr.DropHP(-25.0f);
		pc.eggCount += 1;
	}
}
