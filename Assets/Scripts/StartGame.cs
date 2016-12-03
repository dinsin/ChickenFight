using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Fire1") > 0 || Input.GetAxis("Fire2") > 0)
		{
			SceneManager.LoadScene(1);
		}
	}
}
