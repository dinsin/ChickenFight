using UnityEngine;
using System.Collections;

public class EggBehavior : MonoBehaviour {
	Rigidbody2D rigidBody = null;
	float bounceEffect = 150.0f;

	public float lifespan = 6.0f;
	public int bounceCount = 2;
	public int playerNumber = 1;
	public int attackPower = 15;

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
			coll.collider.GetComponent<Durability>().HP -= attackPower;
			coll.collider.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounceEffect);
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
