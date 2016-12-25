/* ChickenFight
 * Author: Kevin Zeng, Dinesh Singh, Jon Wu */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScroller : MonoBehaviour {

	public Text swapText;
	Text thisTextComponent;
	RectTransform titleRectTransform;
	Vector3 initialPosition, swapPosition;

	// Use this for initialization
	void Start () {
		thisTextComponent = GetComponent<Text>();
		titleRectTransform = GetComponent<RectTransform>();

		initialPosition = transform.localPosition;
		swapPosition = swapText.transform.localPosition;

		StartCoroutine(ExecuteScroller());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator ExecuteScroller()
	{
		WaitForSeconds wfs = new WaitForSeconds(2.0f);
		bool swapWithTitle = true;
		yield return wfs;

		while (true)
		{
			if (swapWithTitle)
			{
				titleRectTransform.localPosition = Vector3.Lerp(titleRectTransform.localPosition, swapPosition, 0.03f);
				swapText.rectTransform.localPosition = Vector3.Lerp(swapText.rectTransform.localPosition, initialPosition, 0.06f);

				if ((swapText.rectTransform.localPosition - initialPosition).magnitude < 0.2f)
				{
					swapWithTitle = false;
					yield return wfs;
				}
			} else
			{
				titleRectTransform.localPosition = Vector3.Lerp(titleRectTransform.localPosition, initialPosition, 0.06f);
				swapText.rectTransform.localPosition = Vector3.Lerp(swapText.rectTransform.localPosition, swapPosition, 0.03f);

				if ((titleRectTransform.localPosition - initialPosition).magnitude < 0.2f)
				{
					swapWithTitle = true;
					yield return wfs;
				}
			}
			yield return null;
		}
	}

	IEnumerator FadeAndSwap()
	{
		WaitForSeconds wfs2 = new WaitForSeconds(1.2f);
		WaitForSeconds wfs4 = new WaitForSeconds(4.0f);
		bool swapWithTextElement = true;
		
		thisTextComponent.CrossFadeAlpha(0, 0, true);
		thisTextComponent.transform.localPosition = swapPosition;

		while (true)
		{
			if (swapWithTextElement)
			{
				thisTextComponent.CrossFadeAlpha(0, 2.0f, true);
				yield return wfs2;
				swapText.CrossFadeAlpha(1, 1.0f, true);
				swapWithTextElement = false;
			} else
			{
				swapText.CrossFadeAlpha(0, 2.0f, true);
				yield return wfs2;
				thisTextComponent.CrossFadeAlpha(1, 1.0f, true);
				swapWithTextElement = true;
			}
			yield return wfs4;
		}
	}
}
