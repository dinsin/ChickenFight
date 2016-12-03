using UnityEngine;
using System.Collections;

public class WrapContent : MonoBehaviour {

	ParticleSystem ps;
	float changeMeter = 0.015f;

	void Start()
	{
		ps = GetComponent<ParticleSystem>();
	}

	void Update() {
		Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

		if (viewportPosition.x < -changeMeter)
		{
			AttemptPause();
			viewportPosition.x = 1 + changeMeter;
		}
		else if (viewportPosition.x > 1 + changeMeter)
		{
			AttemptPause();
			viewportPosition.x = -changeMeter;
		}

		if (viewportPosition.y < -changeMeter)
		{
			AttemptPause();
			viewportPosition.y = 1 + changeMeter;
		} else if (viewportPosition.y > 1 + changeMeter)
		{
			AttemptPause();
			viewportPosition.y = -changeMeter;
		}

		transform.position = Camera.main.ViewportToWorldPoint(viewportPosition);
		if (ps && ps.isPaused)
			ps.Play();
	}

	void AttemptPause()
	{
		if (ps)
			ps.Pause();
	}
}