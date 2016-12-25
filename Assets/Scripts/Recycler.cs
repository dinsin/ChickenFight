/* ChickenFight
 * Author: Kevin Zeng, Dinesh Singh, Jon Wu */
using UnityEngine;
using System.Collections;

public class Recycler : MonoBehaviour {

	Vector3 mTranslation;   // Coordinates of respawn
	public Vector3 mFlowDirection = Vector3.right;
	public BeltDisconnector bder;
	Collider2D cd;
	Transform[] connectedTransforms;

	// Use this for initialization
	void Start () {
		mTranslation = transform.localPosition;

		GameObject theGO = new GameObject(transform.name);
		theGO.transform.parent = transform.parent;
		WrapContent wc = theGO.AddComponent<WrapContent>();
		wc.changeOffsetMultipler = 2;

		transform.parent = theGO.transform;
		transform.parent.localPosition = mTranslation;

		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
		cd = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.parent.localPosition += mFlowDirection * Time.deltaTime;
		if (bder && cd.bounds.Intersects(bder.GetBounds()))
		{
			cd.enabled = false;
		} else
		{
			cd.enabled = true;
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
