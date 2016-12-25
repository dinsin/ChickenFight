/* ChickenFight
 * Author: Kevin Zeng, Dinesh Singh, Jon Wu */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RotateShadow : MonoBehaviour {

	Vector2 originalRotation;
	Shadow shadowUI;

	// Use this for initialization
	void Start () {
		shadowUI = GetComponent<Shadow>();
		originalRotation = shadowUI.effectDistance;
	}
	
	// Update is called once per frame
	void Update () {
		float rotatespeed = Time.time * 2 * Mathf.PI;
		shadowUI.effectDistance = new Vector2(Mathf.Sin(rotatespeed) * originalRotation.x, Mathf.Cos(rotatespeed) * originalRotation.y);
	}
}
