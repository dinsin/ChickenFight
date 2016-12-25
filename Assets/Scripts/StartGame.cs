/* ChickenFight
 * Author: Kevin Zeng, Dinesh Singh, Jon Wu */
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartGame : MonoBehaviour {

	public StagePreviewCycle spc;
	public Text mStageFlavorText;

	int stageCount = 4;
	int stageSelect = 0;
	Text stageSelectText;
	float previousLeftRightAmount;
	float timeBlock;	// Prevent auto-load on start

	// Use this for initialization
	void Start () {
		stageSelectText = GetComponent<Text>();
		timeBlock = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (timeBlock < 0 && ( Input.GetAxis("Fire1") > 0 || Input.GetAxis("Fire2") > 0))
		{
			if (stageSelect == 0)
			{
				SceneManager.LoadScene((int)(Random.value * stageCount) + 2);
			} else
			{
				SceneManager.LoadScene(stageSelect + 1);
			}
		}
		float currentLeftRightAmount = Input.GetAxis("Horizontal1") + Input.GetAxis("Horizontal2") + Input.GetAxis("Vertical1") + Input.GetAxis("Vertical2");
		if (currentLeftRightAmount <= -1 && previousLeftRightAmount > -1)
		{
			stageSelect -= 1;
			if (stageSelect < 0)
			{
				stageSelect = stageCount;
			}
			if (spc)
				spc.SetImage(stageSelect);
		} else if (currentLeftRightAmount >= 1 && previousLeftRightAmount < 1)
		{
			stageSelect += 1;
			if (stageSelect > stageCount)
			{
				stageSelect = 0;
			}
			if (spc)
				spc.SetImage(stageSelect);
		}
		if (stageSelect == 1)
		{
			stageSelectText.text = "Farm";
			mStageFlavorText.text = "Fresh from their eggs, the chickens battle at the Farm amongst their fellow animals";
		} else if (stageSelect == 2)
		{
			stageSelectText.text = "Iceberg";
			mStageFlavorText.text = "The chickens attempt to imitate their penguin cousins, facing off in the arctic Iceberg";
		} else if (stageSelect == 3)
		{
			stageSelectText.text = "Desert";
			mStageFlavorText.text = "Under the beating sun of the Desert, the chickens battle as the vultures watch from above...";
		} else if (stageSelect == 4)
		{
			stageSelectText.text = "Factory";
			mStageFlavorText.text = "The chickens struggle to escape the robotic overlord of the Factory";
		} else
		{
			stageSelectText.text = "Random Stage";
			mStageFlavorText.text = "The chickens wander the world, searching for an arena where they might end their conflict";
		}

		if (timeBlock > 0)
			timeBlock -= Time.deltaTime;
		previousLeftRightAmount = currentLeftRightAmount;
	}
}
