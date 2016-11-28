using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EggCounter : MonoBehaviour {

	Text eggText;
	public PlayerController pc;

	// Use this for initialization
	void Start () {
		eggText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		int count = (int)pc.eggCount;
		eggText.text = "x" + count;
	}
}
