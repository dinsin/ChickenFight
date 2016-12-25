/* ChickenFight
 * Author: Kevin Zeng, Dinesh Singh, Jon Wu */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloatingTextBoxScript : MonoBehaviour {

	public bool SetToDestroy;
	public float RelativeTextFloating;
	Vector3 originalPosition;
	Text textComponent;
	string overwriteText;

	// Use this for initialization
	void Start () {
		textComponent = GetComponent<Text>();
		originalPosition = transform.localPosition;
	}

	// Update is called once per frame
	void Update () {
		transform.localPosition = originalPosition + Vector3.up * RelativeTextFloating;
		textComponent.text = overwriteText;
		if (SetToDestroy)
		{
			Destroy(gameObject);
		}
	}

	public void ChangeText(string overwriteText)
	{
		this.overwriteText = overwriteText;
	}
}
