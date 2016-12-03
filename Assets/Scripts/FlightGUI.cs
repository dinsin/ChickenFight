using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlightGUI : MonoBehaviour {

	public PlayerController pc;
	Image gui;
	float maxTransparency = 0.4f;

	// Use this for initialization
	void Start () {
		gui = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		float flightPercent = pc.GetFlightPercentage();
		if (Input.GetAxis("Vertical" + pc.playerNumber) > 0)
//		if (flightPercent < 0.99f)
		{
			gui.color = new Color(1, 1, 1, Mathf.Min(gui.color.a + 0.10f, maxTransparency));
		} else
		{
			gui.color = new Color(1, 1, 1, Mathf.Max(gui.color.a - 0.009f, 0));
		}
		gui.fillAmount = flightPercent;
		//transform.localScale = new Vector3(1, flightPercent, 1);
	}
}
