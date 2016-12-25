/* ChickenFight
 * Author: Kevin Zeng, Dinesh Singh, Jon Wu */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeTextCoroutine : MonoBehaviour {

	Text mTextElement;

	// Use this for initialization
	IEnumerator Start () {
		mTextElement = GetComponent<Text>();
		yield return new WaitForSeconds(3);
		mTextElement.CrossFadeAlpha(0, 2.0f, true);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
