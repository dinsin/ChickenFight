/* ChickenFight
 * Author: Kevin Zeng, Dinesh Singh, Jon Wu */
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AudioScriptCarryOver : MonoBehaviour {

	float fireButtonTrigger;
	bool escapeButtonTrigger;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		if (FindObjectOfType<AudioScriptCarryOver>() != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
		SceneManager.activeSceneChanged += AudioCarryOverScene;
	}
	
	// Update is called once per frame
	void Update () {
		float fireButton = Input.GetAxis("Fire1") + Input.GetAxis("Fire2");
		if (fireButton != 0 && fireButtonTrigger == 0)
		{
			if (SceneManager.GetActiveScene().buildIndex == 0)
			{
				SceneManager.LoadScene(1);
			}
		}
		bool escapeButton = Input.GetKeyDown(KeyCode.Escape);
		if (escapeButton && !escapeButtonTrigger)
		{
			if (SceneManager.GetActiveScene().buildIndex == 0)
			{
				Application.Quit();
			}
			if (SceneManager.GetActiveScene().buildIndex == 1)
			{
				SceneManager.LoadScene(0);
			}
		}
		fireButtonTrigger = fireButton;
		escapeButtonTrigger = escapeButton;
	}

	void OnDestroy()
	{
		SceneManager.activeSceneChanged -= AudioCarryOverScene;
	}

	void AudioCarryOverScene(Scene previousScene, Scene newScene)
	{
		if (newScene.buildIndex > 1)
			Destroy(gameObject);
	}
}
