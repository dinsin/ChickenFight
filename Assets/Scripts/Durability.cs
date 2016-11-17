using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Durability : MonoBehaviour {

	public int maxHP = 300;
	public int HP;
	public Slider HPBar = null;
	bool deadState = false;

	// Use this for initialization
	void Start () {
		HP = maxHP;
	}
	
	// Update is called once per frame
	void Update () {
		if (deadState)
		{
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
			//Destroy(gameObject);
		}
		if (HPBar != null)
		{
			HPBar.value = HP;
		}
	}

	public bool IsDead()
	{
		return deadState;
	}
}
