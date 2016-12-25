/* ChickenFight
 * Author: Kevin Zeng, Dinesh Singh, Jon Wu */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerUpToken : MonoBehaviour {

	public enum PowerType { ExtraEggs, SuperShot, EggSupply, Health, SuperSpeed };
	public FloatingTextBoxScript FloatingTextBox;

	public PowerType powertype;
	public GameObject powerupSound;
	Animator animator;
	float duration;

	// Use this for initialization
	void Start () {
		duration = 10.0f + Random.value * 5.0f;
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (duration > 0)
		{
			duration -= Time.deltaTime;
			if (duration <= 0)
			{
				animator.SetTrigger("Shrink");
			}
		} else {
			if (transform.localScale.z <= 0)
			{
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D cd)
	{
		PowerUpCollector collector = cd.GetComponent<PowerUpCollector>();
		if (collector)
		{
			FloatingTextBoxScript ftbs = (FloatingTextBoxScript)Instantiate(FloatingTextBox, FindObjectOfType<MainGUICanvas>().transform);
			ftbs.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.position);

			if (powertype == PowerType.ExtraEggs)
			{
				ftbs.ChangeText("Multi\nShot!");
				collector.GetExtraEggThrows();
			}
			else if (powertype == PowerType.SuperShot)
			{
				ftbs.ChangeText("Super\nShot!");
				collector.GetSuperShot();
			}
			else if (powertype == PowerType.EggSupply)
			{
				ftbs.ChangeText("Max\nEggs!");
				collector.GetEggSupply();
			}
			else if (powertype == PowerType.SuperSpeed)
			{
				ftbs.ChangeText("Super\nSpeed!");
				collector.GetExtremeSpeed();
			}
			else
			{
				ftbs.ChangeText("Health\nUP!");
				collector.GetHPBoost();
			}
			Instantiate(powerupSound);

			Destroy(gameObject);
		}
	}
}
