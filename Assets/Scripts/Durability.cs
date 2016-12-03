using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Durability : MonoBehaviour {

	public int maxHP = 300;
	public float HP;
	public RoundTracker rt;
	public Durability otherPlayer;
	bool deadState = false;
	float regenAmount = 0.2f;
	ScreenShake shaker;

	// Use this for initialization
	void Start () {
		HP = maxHP;
		shaker = FindObjectOfType<ScreenShake>();
	}
	
	// Update is called once per frame
	void Update () {
		if (deadState)
		{
			if (rt.FinishedGame())
			{
				return;
			}
			HP += Time.deltaTime * (maxHP * regenAmount);	//Recover 20% each second when dead: 5 second recovery
			if (HP >= maxHP)
			{
				HP = maxHP;
				deadState = false;
				GetComponent<SpriteRenderer>().color = Color.white;
			}
		} else if (HP <= 0)
		{
			//Instantiate animation for death
			shaker.SetShakeStrength(5.0f);
			GetComponent<SoundBank>().PlaySound(2);

			//Destroy gameObject
			SetDead();
			otherPlayer.SetDead();
			rt.Increment();
			//Destroy(gameObject);
		}
	}

	public void SetDead()
	{
		deadState = true;
		GetComponent<SpriteRenderer>().color = Color.grey;
	}

	public bool IsDead()
	{
		return deadState;
	}
}
