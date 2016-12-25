/* ChickenFight
 * Author: Kevin Zeng, Dinesh Singh, Jon Wu */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StagePreviewCycle : MonoBehaviour {

	public Sprite[] images;
	Image imageComponenet;

	// Use this for initialization
	void Start () {
		imageComponenet = GetComponent<Image>();
	}

	public void SetImage(int imageIndex)
	{
		if (imageIndex > images.Length)
		{
			// Resort to last valid index
			imageIndex = images.Length - 1;
		}
		if (imageIndex < 0)
		{
			// Return on invalid image count
			return;
		}
		imageComponenet.sprite = images[imageIndex];
	}
}
