using UnityEngine;
using System.Collections;

public class WrapContent : MonoBehaviour {

	float changeMeter = 0.015f;

	void Update() {
		Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

		if (viewportPosition.x < -changeMeter)
		{
			viewportPosition.x = 1 + changeMeter;
		}
		else if (viewportPosition.x > 1 + changeMeter)
		{
			viewportPosition.x = -changeMeter;
		}

		if (viewportPosition.y < -changeMeter)
		{
			viewportPosition.y = 1 + changeMeter;
		} else if (viewportPosition.y > 1 + changeMeter)
		{
			viewportPosition.y = -changeMeter;
		}

		transform.position = Camera.main.ViewportToWorldPoint(viewportPosition);
	}
}