/* ChickenFight
 * Author: Kevin Zeng, Dinesh Singh, Jon Wu */
using UnityEngine;
using System.Collections;

public class WrapContent : MonoBehaviour {
	
	ParticleSystem ps;
	float changeOffset = 0.015f;
	public float changeOffsetMultipler = 1;

	void Start()
	{
		ps = GetComponent<ParticleSystem>();
	}

	void Update() {
		Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

		if (viewportPosition.x < -changeOffset * changeOffsetMultipler)
		{
			AttemptPause();
			viewportPosition.x = 1 + changeOffset * changeOffsetMultipler + viewportPosition.x;
		}
		else if (viewportPosition.x > 1 + changeOffset * changeOffsetMultipler)
		{
			AttemptPause();
			viewportPosition.x = -changeOffset * changeOffsetMultipler - (1 - viewportPosition.x);
		}

		if (viewportPosition.y < -changeOffset * changeOffsetMultipler)
		{
			AttemptPause();
			viewportPosition.y = 1 + changeOffset * changeOffsetMultipler + viewportPosition.y;
		} else if (viewportPosition.y > 1 + changeOffset * changeOffsetMultipler)
		{
			AttemptPause();
			viewportPosition.y = -changeOffset * changeOffsetMultipler - (1 - viewportPosition.y);
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