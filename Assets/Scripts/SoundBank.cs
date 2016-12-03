using UnityEngine;
using System.Collections;

public class SoundBank : MonoBehaviour {

	public AudioClip flap, hit, death;
	AudioSource src;

	// Use this for initialization
	void Start () {
		src = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlaySound(int flag)
	{
		if (flag == 1)
		{
			src.clip = hit;
			src.Play();
		} else if (flag == 2)
		{
			src.clip = death;
			src.Play();
		} else if (flag == 3)
		{
			src.clip = flap;
			if (!src.isPlaying)
				src.Play();
		}
	}
}
