/* ChickenFight
 * Author: Kevin Zeng, Dinesh Singh, Jon Wu */
using UnityEngine;
using System.Collections;

public class BeltDisconnector : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Bounds GetBounds()
	{
		return GetComponent<Collider2D>().bounds;
	}

	void OnTriggerEnter2D(Collider2D cd)
	{
		if (cd.transform.parent != null)
		{
			cd.transform.parent = null;
			cd.GetComponent<Rigidbody2D>().AddForce((transform.position - cd.transform.position) * 160);
		}
	}

	void OnTriggerStay2D(Collider2D cd)
	{
		if (cd.transform.parent != null)
		{
			cd.transform.parent = null;
		}
	}
}
