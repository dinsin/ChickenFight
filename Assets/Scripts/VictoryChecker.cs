/* ChickenFight
 * Author: Kevin Zeng, Dinesh Singh, Jon Wu */
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class VictoryChecker : MonoBehaviour {
	public GameObject PauseMenu;
	public RoundTracker rt;
	float mEndTime = 5.0f;
	float endGameTimer = 0;
	Text txt;

	void Start ()
	{
		if (PauseMenu)
			PauseMenu.SetActive(false);
		txt = GetComponent<Text>();
		StartCoroutine(StartGameTime());
	}

	IEnumerator StartGameTime()
	{
		WaitForSeconds wfs = new WaitForSeconds(1.0f);
		txt.text = "Get Ready";
		StartCoroutine(RegrowTextSize(2));
		yield return wfs;
		txt.text = "3";
		StartCoroutine(RegrowTextSize(2));
		StartCoroutine(ShakeEmUp(2));
		yield return wfs;
		txt.text = "2";
		StartCoroutine(RegrowTextSize(3));
		StartCoroutine(ShakeEmUp(3));
		yield return wfs;
		txt.text = "1";
		StartCoroutine(RegrowTextSize(4));
		StartCoroutine(ShakeEmUp(4));
		yield return wfs;
		txt.text = "FIGHT!!!";
		StartCoroutine(RegrowTextSize(8));
		StartCoroutine(ShakeEmUp(8));
		yield return wfs;
		txt.text = "";
	}

	IEnumerator RegrowTextSize(int sizeBoost)
	{
		int initSize = txt.fontSize;
		txt.fontSize = initSize * sizeBoost;
		while (txt.fontSize > initSize)
		{
			txt.fontSize = (int)Mathf.Lerp(txt.fontSize, initSize, 0.2f);
			yield return null;
		}
		yield return null;
	}

	IEnumerator ShakeEmUp(float shakeStrength)
	{
		Vector3 initPosition = txt.transform.position;
		while (shakeStrength > 0.02f)
		{
			txt.transform.position = initPosition + shakeStrength * (Vector3.right * Mathf.Sin(Time.time * 160f) + Vector3.up * Mathf.Sin(Time.time * 90f));
			yield return null;
			shakeStrength = Mathf.Lerp(shakeStrength, 0, 0.05f);
		}
	}

	// Update is called once per frame
	void Update () {
		if (rt.FinishedGame() == 1)
		{
			if (endGameTimer > 0)
			{
				txt.text = "Marie Wins!\n\n" + "Preparing the next fight...";
				endGameTimer -= Time.deltaTime;
			}
			else if (endGameTimer == 0)
			{
				txt.fontSize /= 2;
				endGameTimer = mEndTime;
			}
			else if (endGameTimer < 0)
			{
				txt.text = "Press 'Fire' for the next\n\nChicken Fight";
				//Reset On Input
				if (Input.GetAxis("Fire1") > 0 || Input.GetAxis("Fire2") > 0)
				{
					RepeatGame();
				}
			}
		}
		else if (rt.FinishedGame() == 2)
		{
			if (endGameTimer > 0)
			{
				txt.text = "Suede Wins!\n\n" + "Preparing the next fight...";
				endGameTimer -= Time.deltaTime;
			}
			else if (endGameTimer == 0)
			{
				txt.fontSize /= 2;
				txt.enabled = true;
				endGameTimer = mEndTime;
			}
			else if (endGameTimer < 0)
			{
				txt.text = "Press 'Fire' for the next\n\nChicken Fight";
				//Reset On Input
				if (Input.GetAxis("Fire1") > 0 || Input.GetAxis("Fire2") > 0)
				{
					RepeatGame();
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			//Activate pause menu
			ActivatePauseMenu();
		}
		if (PauseMenu.activeInHierarchy)
		{
			Time.timeScale = 0;
		}
	}

	void RepeatGame()
	{
		SceneManager.LoadScene(1);
	}

	public void ActivatePauseMenu()
	{
		if (PauseMenu)
		{
			if (PauseMenu.activeInHierarchy)
			{
				Time.timeScale = 1;
				PauseMenu.SetActive(false);
			}
			else
			{
				Time.timeScale = 0;
				PauseMenu.SetActive(true);
			}
		}
	}

	public void ReturnToTitle()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(0);
	}
}
