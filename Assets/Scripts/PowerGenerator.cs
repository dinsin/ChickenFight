using UnityEngine;
using System.Collections;

public class PowerGenerator : MonoBehaviour {

	public PowerUpToken[] powers;
	public float BaseSpawnFrequency = 10.0f;
	public float RandomSpawnFrequency = 5.0f;
	float generateTimer;
	float randomSpeed = 20.0f;
	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		Vector2 randomForce = new Vector2(Random.value - 0.5f, Random.value - 0.5f);
		rb.velocity = randomForce;
		generateTimer = Random.value * RandomSpawnFrequency + BaseSpawnFrequency;
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = rb.velocity.normalized * randomSpeed;
		if (generateTimer > 0)
		{
			generateTimer -= Time.deltaTime;
			if (generateTimer <= 0)
			{
				// Generate Power Up
				Instantiate(powers[(int)(Random.value * powers.Length)], transform.position, Quaternion.identity);

				generateTimer = Random.value * RandomSpawnFrequency + BaseSpawnFrequency;
			}
		}
	}
}
