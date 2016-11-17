using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Durability : MonoBehaviour {

	public int HP = 100;
	public Slider HPBar = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (HP <= 0)
		{
			//Instantiate animation for death

			//Destroy gameObject
			Destroy(gameObject);
		}
		if (HPBar != null)
		{
			HPBar.value = HP;
		}
	}
}
