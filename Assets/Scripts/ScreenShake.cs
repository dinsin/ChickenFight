/* ChickenFight
 * Author: Kevin Zeng, Dinesh Singh, Jon Wu */
using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour {

	Vector3 startPosition;
	float shakeStrength = 0f;
	float shakeDecay = 0.1f;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		shakeStrength = Mathf.Lerp(shakeStrength, 0, shakeDecay);

		// multiply INSIDE sine wave = faster frequency, speed, duration of period
		// multiply OUTSIDE sine wave = distance, amplitude
		transform.position = startPosition
				+ shakeStrength * (
					transform.right * Mathf.Sin(Time.time * 64f) +
					transform.up * Mathf.Sin(Time.time * 36f)
				);
	}

	public void SetShakeStrength(float strength, float decay)
	{
		shakeStrength = strength;
		shakeDecay = decay;
	}

	public bool IsShakeZero()
	{
		return shakeStrength < 0.05f;
	}
}
