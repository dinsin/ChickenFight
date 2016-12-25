/* ChickenFight
 * Author: Kevin Zeng, Dinesh Singh, Jon Wu */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

	AudioSource audiosource;
	public RoundTracker rt;
	public Durability player1, player2;
	public float MaxTime = 99;
	float timeLeft;
	public Image timerImage;
	public Text txt;
	bool turningRed;

	// Use this for initialization
	void Start () {
		timeLeft = MaxTime;
		turningRed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!(player1.IsDead() || player2.IsDead()))
			timeLeft -= Time.deltaTime;
		else
			ResetTimer();
		if (timeLeft <= 0)
		{
			audiosource = GetComponent<AudioSource>();
			if (audiosource && !audiosource.isPlaying)
			{
				audiosource.Play();
			}
			if (player1.HP > player2.HP)
			{
				rt.Increment(1);
			} else if (player2.HP > player1.HP)
			{
				rt.Increment(2);
			}
			ResetTimer();
			player1.DieFromTime();
			player2.DieFromTime();
		}
		if (timeLeft < 10)
		{
			string timedisplay = (int)timeLeft + "." + (int)(timeLeft % 1 * 10);
			txt.text = "" + timedisplay;
			txt.text = "" + (int)timeLeft;
			if (!turningRed)
			{
				turningRed = true;
				StartCoroutine(TurnRed());
			}
		} else
		{
			txt.text = "" + (int)timeLeft;
		}
		timerImage.fillAmount = timeLeft / MaxTime;
	}

	IEnumerator TurnRed()
	{
		int regularSize = txt.fontSize;
		while (turningRed)
		{
			float timeGlow = Mathf.Sin(Time.time * Mathf.PI);
			timeGlow = Mathf.Pow(timeGlow, 4);
			txt.color = Color.Lerp(Color.black, Color.red, timeGlow);
			timeGlow = Mathf.Pow(timeGlow, 32);
			txt.fontSize = (int)Mathf.Lerp(regularSize, regularSize * 2, timeGlow);
			yield return null;
		}
		txt.color = Color.black;
		txt.fontSize = regularSize;
	}

	public void ResetTimer()
	{
		timeLeft = MaxTime;
		turningRed = false;
	}
}
