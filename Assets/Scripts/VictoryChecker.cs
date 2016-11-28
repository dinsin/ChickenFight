using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VictoryChecker : MonoBehaviour {
	public RoundTracker rt1, rt2;
	Text txt;

	void Start ()
	{
		txt = GetComponent<Text>();
		txt.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		if (rt1.FinishedGame())
		{
			txt.enabled = true;
			txt.text = "Player 1 Wins!";
			//Do Reset After Time
		}
		else if (rt2.FinishedGame())
		{
			txt.enabled = true;
			txt.text = "Player 2 Wins!";
			//Do Reset After Time
		}
	}
}
