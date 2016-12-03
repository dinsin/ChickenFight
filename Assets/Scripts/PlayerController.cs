using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	Rigidbody2D rb;
	SpriteRenderer spriteRenderer;
	Vector2 inputVector;

	public float eggFireInterval = 0.3f;
	public float maxEggCount = 30.0f;
	public float eggCount = 0.0f;
	public float stunLength = 0.0f;
	float eggWait;
	float eggRegen = 0.65f;
	float recoil = 300.0f;
	float characterBounce = 400.0f;

	public GameObject eggPrefab;
	Durability durability;

	public float maxFiniteFlight = 3.0f;
	float finiteFlight = 0.0f;
	public float moveSpeed = 5.0f;
	public float jumpSpeed = 8.0f;
	public Vector2 throwSpeed = new Vector2(4.0f, 5.0f);
	public int playerNumber = 1;
	bool facingRight;

	// Use this for initialization
	void Start() {
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		facingRight = spriteRenderer.flipX;

		durability = GetComponent<Durability>();
		Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer);
		eggCount = maxEggCount;
		finiteFlight = maxFiniteFlight;
	}
	
	// Update is called once per frame
	void Update() {
		
		inputVector = Vector2.zero;

		if (stunLength <= 0)
		{
			// Set vertical velocity to jump
			if (Input.GetAxis("Vertical" + playerNumber) > 0)
			{
				if (finiteFlight > 0)
				{
					finiteFlight = Mathf.Max(finiteFlight - Time.deltaTime, 0);
					rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
					GetComponent<SoundBank>().PlaySound(3);
				}
			}
			else if (rb.velocity.y <= 0)
			{
				finiteFlight = Mathf.Min(finiteFlight + Time.deltaTime * Mathf.Clamp(1 + rb.velocity.y, 0.0f, 1.0f) * 1.6f, maxFiniteFlight);
			}

			// Set inputVector left and right on input
			inputVector += new Vector2(Input.GetAxis("Horizontal" + playerNumber), 0.0f);

			// Produce an egg
			if (Input.GetAxis("Fire" + playerNumber) > 0)
			{
				if (eggCount >= 1 && eggWait <= 0 && (durability != null && !durability.IsDead()))
				{
					eggCount -= 1;
					MakeEgg();
					eggWait = eggFireInterval;
				}
			}
			else
			{
				eggCount = Mathf.Min(eggCount + Time.deltaTime * eggRegen, maxEggCount);
			}
		} else
		{
			stunLength -= Time.deltaTime;
		}

		inputVector.Normalize();
		eggWait -= Time.deltaTime;
	}

	void MakeEgg() {
		
		// Create the game object for an egg
		GameObject egg = (GameObject)Instantiate(eggPrefab, transform.position, Quaternion.identity);
		egg.layer = gameObject.layer;
		Rigidbody2D eggrb = egg.GetComponent<Rigidbody2D> (); 

		// Set the velocity of the egg, add bonus velocity based on player velocity
		eggrb.velocity = new Vector2(throwSpeed.x * (facingRight ? 1 : -1) - rb.velocity.x * 0.4f, throwSpeed.y - Mathf.Abs(rb.velocity.x) * 0.5f);
		eggrb.angularVelocity = (facingRight ? -1 : 1) * 360;
		if (Mathf.Abs(rb.velocity.x) < 0.2f)
		{
			// if player has small or no x-velocity, add more bounce to egg
			egg.GetComponent<EggBehavior>().bounceCount += 2;
		}

		// push player for recoil on shot
		rb.AddForce(new Vector2(-0.5f * (facingRight ? 1 : -1), 0.5f) * recoil);
	}

	void FixedUpdate() {
		// apply friction to x-axis
//		rb.velocity = new Vector2(inputVector.x * moveSpeed, rb.velocity.y);
		rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(inputVector.x * moveSpeed, rb.velocity.y), 0.2f);

		if (rb.velocity.x < 0) {
			facingRight = true;
		} 
		else if (rb.velocity.x > 0) {
			facingRight = false;
		}

		spriteRenderer.flipX = facingRight;
	}

	void OnCollisionEnter2D(Collision2D cd)
	{
		if (cd.collider.CompareTag("Player")) {
			rb.AddForce((transform.position - cd.collider.transform.position).normalized * characterBounce);
			if (transform.position.y - cd.collider.transform.position.y > 0.2f)
			{
				//cd.collider.GetComponent<PlayerController>().stunLength = 1.0f;
			}
		}
	}

	void OnCollisionStay2D(Collision2D cd)
	{
		if (cd.collider.CompareTag("Wall") && rb.velocity.y <= 0)
		{
			//finiteFlight = maxFiniteFlight;
		}
	}

	public float GetFlightPercentage()
	{
		return finiteFlight / maxFiniteFlight;
	}
}
