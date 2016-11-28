using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Durability : MonoBehaviour {

	public int maxHP = 300;
	public int HP;
	public RoundTracker rt;
	bool deadState = false;

	// Use this for initialization
	void Start () {
		HP = maxHP;
	}
	
	// Update is called once per frame
	void Update () {
		if (deadState)
		{
			if (rt.FinishedGame())
			{
				return;
			}
			HP += 1;
			if (HP >= maxHP)
			{
				HP = maxHP;
				deadState = false;
				GetComponent<SpriteRenderer>().color = Color.white;
			}
		} else if (HP <= 0)
		{
			//Instantiate animation for death

			//Destroy gameObject
			deadState = true;
			GetComponent<SpriteRenderer>().color = Color.grey;
			rt.Increment();
			//Destroy(gameObject);
		}
	}

	public bool IsDead()
	{
		return deadState;
	}
}
