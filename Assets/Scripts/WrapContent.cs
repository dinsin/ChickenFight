using UnityEngine;
using System.Collections;

public class WrapContent : MonoBehaviour {
	
	void Update(){
		
		Vector3 newPosition = transform.position;
		Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

		if (viewportPosition.x < 0 || viewportPosition.x > 1) {
			newPosition.x = -newPosition.x * 0.99f;
		}

		if (viewportPosition.y < 0 || viewportPosition.y > 1) {
			newPosition.y = -newPosition.y * 0.99f;
		}

		transform.position = newPosition;
	}
}