using UnityEngine;
using System.Collections;

public class TiltingPlatform : MonoBehaviour {
	
	public bool MovingSideways, MovingAngled, MovingUpAndDown;

	public float moveAmount = 0;
	float rotationAmount;
	float oscillationSpeed = 0.5f;

	// Use this for initialization
	void Start () {
		GameObject theGO = new GameObject(transform.name);
		theGO.transform.parent = transform.parent;
		transform.parent = theGO.transform;

		rotationAmount = transform.localEulerAngles.z;
		transform.localRotation = Quaternion.identity;
	}
	
	// Update is called once per frame
	void Update () {
		if (MovingAngled)
		{
			transform.parent.localEulerAngles = new Vector3(0, 0, rotationAmount * -Mathf.Sin(Time.time * oscillationSpeed));
		}
		if (MovingSideways)
		{
			transform.parent.position = new Vector3(moveAmount * Mathf.Sin(Time.time * oscillationSpeed), transform.parent.position.y, transform.parent.position.z);
		}
		if (MovingUpAndDown)
		{
			transform.parent.position = new Vector3(transform.parent.position.x, moveAmount * Mathf.Sin(Time.time * oscillationSpeed * 2 - Mathf.PI / 2), transform.parent.position.z);
		}
	}

	void OnCollisionEnter2D(Collision2D cd)
	{
		cd.transform.parent = transform.parent;
		cd.transform.localRotation = Quaternion.identity;
	}

	void OnCollisionExit2D(Collision2D cd)
	{
		cd.transform.parent = null;
		cd.transform.localRotation = Quaternion.identity;
	}
}
