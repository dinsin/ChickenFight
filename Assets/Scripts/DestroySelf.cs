using UnityEngine;
using System.Collections;

public class DestroySelf : MonoBehaviour {

	public float lifetime = 3.0f;
	float mTimeCreated;

	// Use this for initialization
	void Start () {
		mTimeCreated = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - mTimeCreated > lifetime)
		{
			Destroy(gameObject);
		}
	}
}
