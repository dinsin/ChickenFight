using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EggCounter : MonoBehaviour {

	public PlayerController pc;
	float initialWidth;
	RectTransform rt;

	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform>();
		initialWidth = rt.rect.width;
	}
	
	// Update is called once per frame
	void Update () {
		int count = (int)pc.eggCount;

		rt.sizeDelta = new Vector2(initialWidth * (count / pc.maxEggCount), rt.rect.height);
	}
}
