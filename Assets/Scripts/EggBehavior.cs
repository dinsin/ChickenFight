﻿using UnityEngine;
using System.Collections;

public class EggBehavior : MonoBehaviour {
	Rigidbody2D rigidBody = null;
	public float lifespan = 6.0f;
	public int bounceCount = 2;
	public int playerNumber = 1;

	void Start() {
		rigidBody = GetComponent<Rigidbody2D>();
	}

	void Update() {
		lifespan -= Time.deltaTime;
		if (lifespan <= 0) {
			DeathEffect();
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.collider.CompareTag("Wall")){
			bounceCount--;
			if (bounceCount < 0) {
				DeathEffect();
			}
		} else if (coll.collider.GetComponent<Durability>() != null)
		{
			coll.collider.GetComponent<Durability>().HP -= 1;
			DeathEffect();
		}
	}

	void DeathEffect()
	{
		//Instantiate animation for death

		//Destroy game object
		Destroy(gameObject);
	}
}
