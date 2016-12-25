/* ChickenFight
 * Author: Kevin Zeng, Dinesh Singh, Jon Wu */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Durability : MonoBehaviour {

	public int maxHP = 300;
	public float HP;
	public RoundTracker rt;
	public Durability otherPlayer;
	public GameObject deathExplosion;
	bool deadState = false;
	bool finishedGame = false;
	float regenAmount = 0.2f;
	float flashAmount = 0.0f;
	float minTransparency = 0.4f;
	ScreenShake shaker;
	SpriteRenderer sr;
	Animator animator;
	SoundBank sounds;
	PlayerController pc;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
		shaker = FindObjectOfType<ScreenShake>();
		animator = GetComponent<Animator>();
		sounds = GetComponent<SoundBank>();
		pc = GetComponent<PlayerController>();

		HP = maxHP * (1 - regenAmount * 4.8f);
		SetDead();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (deadState)
		{
			if (rt && rt.FinishedGame() != 0 && rt.FinishedGame() != pc.playerNumber)
			{
				if (!finishedGame)
				{
					if (deathExplosion)
					{
						GameObject go = (GameObject)(Instantiate(deathExplosion, transform.position, Quaternion.identity));
						ParticleSystem ps = go.GetComponent<ParticleSystem>();
						ps.startLifetime *= 2;
						ps.startSize *= 3;
						ps.gravityModifier = 0.1f;
						go.GetComponent<ParticleSystemRenderer>().sortingOrder = 11;
					}
					sr.color = Color.white;
					if (animator)
						animator.SetTrigger("TriggerDead");
					finishedGame = true;
				}
				return;
			}
			HP += Time.deltaTime * (maxHP * regenAmount);   //Recover 20% each second when dead: 5 second recovery
			if (HP >= maxHP)
			{
				HP = maxHP;
				deadState = false;
				sr.color = Color.white;
			} else
			{
				float fraction = HP / maxHP;
				Color droppedColor = Color.white;
				droppedColor.a = fraction * fraction * (1 - minTransparency) + minTransparency;
				sr.color = droppedColor;
			}
		}
		ContinueFlash();
	}

	public void DropHP(float damagePoints)
	{
		if (!deadState)
		{
			HP = Mathf.Clamp(HP - damagePoints, 0, maxHP);

			if (damagePoints > 0)
			{
				if (HP > 0)
				{
					shaker.SetShakeStrength(0.2f, 0.1f);
					sounds.PlaySound(1);
					//Create FLASHING effect
					SetFlash(0.3f);
					GetComponent<PlayerController>().stunLength += 0.1f;
				}
				else
				{
					shaker.SetShakeStrength(0.9f, 0.023f);
					sounds.PlaySound(2);
					SetDead();
					SetFlash(0.45f);
					GetComponent<PlayerController>().stunLength = 0.45f;
					if (otherPlayer)
						otherPlayer.SetDead();
					if (rt)
						rt.Increment(pc.playerNumber % 2 + 1);
				}
			}
		}
	}

	void SetFlash(float flashLength)
	{
		flashAmount = flashLength;
		animator.SetFloat("FlashAmount", flashAmount);
	}

	void ContinueFlash()
	{
		if (flashAmount > 0)
		{
			flashAmount -= Time.deltaTime;
			animator.SetFloat("FlashAmount", flashAmount);
			if (flashAmount < 0)
			{
				sr.color = Color.white;
			} else
			{
				sr.color = Random.ColorHSV(0, 1, 0, 1, 0.5f, 1);
			}
		}
	}

	public void SetDead()
	{
		deadState = true;
		flashAmount = 0;
	}

	public void DieFromTime()
	{
		if (HP < otherPlayer.HP)
		{
			deadState = true;
			SetFlash(0.45f);
		} else
		{
			SetDead();
		}
	}

	public bool IsDead()
	{
		return deadState;
	}

	public bool IsFinished()
	{
		return deadState && rt && rt.FinishedGame() != 0;
	}
}
