using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour {

	Vector3 startPosition;
	float shakeStrength = 0f;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		shakeStrength = Mathf.Lerp(shakeStrength, 0, 0.1f);

		// multiply INSIDE sine wave = faster frequency, speed, duration of period
		// multiply OUTSIDE sine wave = distance, amplitude
		transform.position = startPosition
				+ shakeStrength* (
					transform.right* Mathf.Sin(Time.time* 10f) * 0.2f +
					transform.up* Mathf.Sin(Time.time* 10f) * 0.2f
				);
	}

	public void SetShakeStrength(float strength)
	{
		shakeStrength = strength;
	}
}
