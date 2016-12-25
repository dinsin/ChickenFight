/* ChickenFight
 * Author: Kevin Zeng, Dinesh Singh, Jon Wu */
using UnityEngine;
using System.Collections;

public class TiltingPlatform : MonoBehaviour {
	
	public bool MovingSideways, MovingAngled, MovingUpAndDown;
	Vector3 originalPosition;
	public float oscillationSpeed = 0.5f;
	public float moveAmountX = 0;
	public float moveAmountY = 0;
	public float offset = 0;
	float rotationAmount;

	// Use this for initialization
	void Start () {
		GameObject theGO = new GameObject(transform.name);
		theGO.transform.parent = transform.parent;
		originalPosition = transform.localPosition;

		rotationAmount = transform.localEulerAngles.z;

		transform.parent = theGO.transform;

		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
	}
	
	// Update is called once per frame
	void Update () {
		if (MovingAngled)
		{
			transform.parent.localEulerAngles = new Vector3(0, 0, rotationAmount * -Mathf.Sin(Time.time * oscillationSpeed + offset * Mathf.PI));
		}
		if (MovingSideways)
		{
			transform.parent.position = new Vector3(originalPosition.x + moveAmountX * Mathf.Sin(Time.time * oscillationSpeed), originalPosition.y, originalPosition.z);
		}
		if (MovingUpAndDown)
		{
			transform.parent.position = new Vector3(originalPosition.x, originalPosition.y + moveAmountY * Mathf.Sin(Time.time * oscillationSpeed * 2 - Mathf.PI / 2), originalPosition.z);
		}
	}

	void OnCollisionEnter2D(Collision2D cd)
	{
		cd.collider.transform.parent = transform.parent;
		cd.collider.transform.localRotation = Quaternion.identity;
	}

	void OnCollisionExit2D(Collision2D cd)
	{
		cd.collider.transform.parent = null;
		cd.collider.transform.localRotation = Quaternion.identity;
	}
}
