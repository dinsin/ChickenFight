using UnityEngine;
using System.Collections;

public class EggBehavior : MonoBehaviour {
	Rigidbody2D rigidBody = null;
	public float lifespan = 5.0f;
	int bounceCount = 4;

	void Start() {
		rigidBody = GetComponent<Rigidbody2D>();
	}

	void Update() {
		lifespan -= Time.deltaTime;
		if (lifespan <= 0) {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.collider.CompareTag("Wall")){
			bounceCount--;
			if (bounceCount < 0) {
				Destroy (gameObject);
			}
		}
	}
}
