using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoundTracker : MonoBehaviour {

	public Image track1, track2, track3;
	int tracker = 0;

	// Use this for initialization
	void Start () {
		track1.enabled = false;
		track2.enabled = false;
		track3.enabled = false;
	}

	public void Increment()
	{
		tracker += 1;
		if (tracker > 0)
		{
			track1.enabled = true;
		}
		if (tracker > 1)
		{
			track2.enabled = true;
		}
		if (tracker > 2)
		{
			track3.enabled = true;
			//Finish Game
		}
	}

	public bool FinishedGame()
	{
		return tracker > 2;
	}
}
