using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Durability : MonoBehaviour {

	public int maxHP = 300;
	public float HP;
	public Sprite cooked;
	public Sprite whitened;
	Sprite origin;
	public RoundTracker rt;
	public Durability otherPlayer;
	bool deadState = false;
	float regenAmount = 0.2f;
	float flashAmount = 0.0f;
	ScreenShake shaker;
	SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		HP = maxHP;
		sr = GetComponent<SpriteRenderer>();
		origin = sr.sprite;
		shaker = FindObjectOfType<ScreenShake>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (deadState)
		{
			if (rt && rt.FinishedGame())
			{
				sr.color = Color.white;
				sr.sprite = cooked;
				return;
			}
			HP += Time.deltaTime * (maxHP * regenAmount);   //Recover 20% each second when dead: 5 second recovery
			if (HP >= maxHP)
			{
				HP = maxHP;
				deadState = false;
				sr.color = Color.white;
			}
		}
		else
		{
			ContinueFlash();
		}
	}

	public void DropHP(float damagePoints)
	{
		if (!deadState)
		{
			HP = Mathf.Clamp(HP - damagePoints, 0, maxHP);

			if (HP > 0)
			{
				shaker.SetShakeStrength(1.0f);
				GetComponent<SoundBank>().PlaySound(1);
				//Create FLASHING effect
				SetFlash(0.3f);
				GetComponent<PlayerController>().stunLength += 0.1f;
			} else
			{
				shaker.SetShakeStrength(5.0f);
				GetComponent<SoundBank>().PlaySound(2);
				SetDead();
				if (otherPlayer)
					otherPlayer.SetDead();

				if (rt)
					rt.Increment();
			}
		}
	}

	void SetFlash(float flashLength)
	{
		flashAmount = flashLength;
	}

	void ContinueFlash()
	{
		if (flashAmount > 0)
		{
			flashAmount -= Time.deltaTime;
			if (flashAmount < 0)
			{
				sr.color = Color.white;
				sr.sprite = origin;
			} else
			{
				sr.color = Random.ColorHSV();
				sr.sprite = whitened;
			}
		}
	}

	public void SetDead()
	{
		deadState = true;
		flashAmount = 0;
		sr.sprite = origin;
		sr.color = Color.grey;
	}

	public bool IsDead()
	{
		return deadState;
	}

	public bool IsFinished()
	{
		return deadState && rt && rt.FinishedGame();
	}
}
