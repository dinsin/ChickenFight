using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EggCounter : MonoBehaviour {

	Image eggText;
	public PlayerController pc;
	float initialWidth;
	RectTransform rt;

	// Use this for initialization
	void Start () {
		eggText = GetComponent<Image>();
		rt = GetComponent<RectTransform>();
		initialWidth = rt.rect.width;
	}
	
	// Update is called once per frame
	void Update () {
		int count = (int)pc.eggCount;

		rt.sizeDelta = new Vector2(initialWidth * (count / pc.maxEggCount), rt.rect.height);
	}
}
