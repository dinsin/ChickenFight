/* ChickenFight
 * Author: Kevin Zeng, Dinesh Singh, Jon Wu */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuFader : MonoBehaviour {

	Text thisTextComponent;

	// Use this for initialization
	void Start () {
		thisTextComponent = GetComponent<Text>();
		StartCoroutine(FadeInAndOut());
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator FadeInAndOut()
	{
		WaitForSeconds wfs2 = new WaitForSeconds(0.8f);
		WaitForSeconds wfs4 = new WaitForSeconds(1.6f);

		while (true)
		{
			thisTextComponent.CrossFadeAlpha(0, 0.5f, true);
			yield return wfs2;
			thisTextComponent.CrossFadeAlpha(1, 0.5f, true);
			yield return wfs4;
		}
	}
}
