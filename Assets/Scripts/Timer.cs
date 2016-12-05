using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

	AudioSource audiosource;
	public RoundTracker rt1, rt2;
	public Durability player1, player2;
	public float MaxTime = 99;
	float timeLeft;
	Text txt;

	// Use this for initialization
	void Start () {
		timeLeft = MaxTime;
		txt = GetComponent<Text>();
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
				rt1.Increment();
			} else if (player2.HP > player1.HP)
			{
				rt2.Increment();
			}
			ResetTimer();
			player1.SetDead();
			player2.SetDead();
		}
		txt.text = "Timer\n" + (int)timeLeft;
	}

	public void ResetTimer()
	{
		timeLeft = MaxTime;
	}
}
