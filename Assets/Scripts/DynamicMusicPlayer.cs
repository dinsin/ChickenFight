/* ChickenFight
 * Author: Kevin Zeng, Dinesh Singh, Jon Wu */
using UnityEngine;
using System.Collections;

public class DynamicMusicPlayer : MonoBehaviour {

	public AudioClip[] LoopBeats;
	AudioSource mAudioPlayer;

	// Use this for initialization
	void Start () {
		mAudioPlayer = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!mAudioPlayer.isPlaying)
		{
			mAudioPlayer.clip = LoopBeats[Random.Range(0, LoopBeats.Length - 1)];
			mAudioPlayer.Play();
		}
	}
}
