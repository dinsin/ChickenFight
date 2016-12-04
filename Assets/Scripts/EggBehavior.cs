using UnityEngine;
using System.Collections;

public class EggBehavior : MonoBehaviour {
	ScreenShake shaker;
	float bounceEffect = 150.0f;

	public GameObject explosionEffect;
	public float lifespan = 6.0f;
	public int bounceCount = 2;
	public int playerNumber = 1;
	public float attackPower = 30;
	public float eggDecay = 0.75f;

	void Start() {
		shaker = FindObjectOfType<ScreenShake>();
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
			GetComponent<AudioSource>().Play();
			attackPower *= eggDecay;
			if (bounceCount < 0) {
				DeathEffect();
			}
		} else if (coll.collider.GetComponent<Durability>() != null)
		{
			coll.collider.GetComponent<Durability>().DropHP(attackPower);
			
			coll.collider.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounceEffect);
			DeathEffect();
		}
	}

	void DeathEffect()
	{
		//Instantiate animation for death
		GameObject explosion = (GameObject)Instantiate(explosionEffect, transform.position, Quaternion.identity);

		//Destroy game object
		Destroy(gameObject);
	}
}
