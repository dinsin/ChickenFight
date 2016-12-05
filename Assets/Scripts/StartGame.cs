using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartGame : MonoBehaviour {

	int stageSelect = 0;
	Text stageSelectText;
	float previousLeftRightAmount;

	// Use this for initialization
	void Start () {
		stageSelectText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Fire1") > 0 || Input.GetAxis("Fire2") > 0)
		{
			if (stageSelect == 0)
			{
				SceneManager.LoadScene((int)(Random.value * 2) + 1);
			} else
			{
				SceneManager.LoadScene(stageSelect);
			}
		}
		float currentLeftRightAmount = Input.GetAxis("Horizontal1") + Input.GetAxis("Horizontal2");
		if (currentLeftRightAmount <= -1 && previousLeftRightAmount > -1)
		{
			stageSelect -= 1;
			if (stageSelect < 0)
			{
				stageSelect = 2;
			}
		} else if (currentLeftRightAmount >= 1 && previousLeftRightAmount < 1)
		{
			stageSelect += 1;
			if (stageSelect > 2)
			{
				stageSelect = 0;
			}
		}
		if (stageSelect == 0)
		{
			stageSelectText.text = "Stage (Random)";
		}
		else
		{
			stageSelectText.text = "Stage " + stageSelect;
		}

		previousLeftRightAmount = currentLeftRightAmount;
	}
}
