/* ChickenFight
 * Author: Kevin Zeng, Dinesh Singh, Jon Wu */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoundTracker : MonoBehaviour {
	public Sprite emptyTrack, firstPlayerTrack, secondPlayerTrack;
	public Image[] tracks;
	int mNumberOfRounds = 3;
	int mRoundNumber = 0;
	float mFullSlow = 3.0f;
	float slowTime;
	int[] rounds;

	// Use this for initialization
	void Start () {
		rounds = new int[mNumberOfRounds];
		for (int i = 0; i < tracks.Length; i++)
		{
			tracks[i].sprite = emptyTrack;
		}
	}

	void Update()
	{
		if (slowTime > 0)
		{
			slowTime -= Time.unscaledDeltaTime;
			if (slowTime <= 0) {
				Time.timeScale = 1;
			}
			else
			{
				Time.timeScale = Mathf.Lerp(1, 0, slowTime / mFullSlow);
			}
		}
	}

	IEnumerator RegrowScaleSize(Transform UIObject, float sizeBoost)
	{
		Vector3 initSize = UIObject.localScale;
		UIObject.localScale = initSize * sizeBoost;
		while (UIObject.localScale.magnitude > initSize.magnitude * 1.02f)
		{
			UIObject.localScale = Vector3.Lerp(UIObject.localScale, initSize, 0.03f);
			yield return null;
		}
		yield return null;
	}

	public void Increment(int playerNumber)
	{
		slowTime = mFullSlow;
		rounds[mRoundNumber] = playerNumber;
		if (playerNumber == 1)
		{
			tracks[mRoundNumber].sprite = firstPlayerTrack;
		} else if (playerNumber == 2)
		{
			tracks[mRoundNumber].sprite = secondPlayerTrack;
		}
		StartCoroutine(RegrowScaleSize(tracks[mRoundNumber].transform, 6));
		mRoundNumber++;
	}

	public int FinishedGame()
	{
		if (mRoundNumber < mNumberOfRounds / 2 + 1)
			return 0;
		int player1Wins = 0;
		int player2Wins = 0;
		for (int i = 0; i < mNumberOfRounds; i++)
		{
			if (rounds[i] == 1)
			{
				player1Wins++;
			} else if (rounds[i] == 2)
			{
				player2Wins++;
			}
		}
		if (player1Wins > player2Wins)
			return 1;
		if (player2Wins > player1Wins)
			return 2;
		return 0;
	}

	public void RepeatGame()
	{
		for (int i = 0; i < mNumberOfRounds; i++)
		{
			rounds[i] = 0;
		}
	}
}
