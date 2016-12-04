using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	Rigidbody2D rb;
	SpriteRenderer spriteRenderer;
	Vector2 inputVector;

	public float eggFireInterval = 0.3f;	// Fire rate of eggs
	public float maxEggCount = 30.0f;
	public float eggCount = 0.0f;
	public float stunLength = 0.0f;
	float eggWait;                  // Current fire rate of eggs
	float eggRegen = 0.75f;			// Affects egg production rate (0.75 seconds to produce an egg)
	float recoil = 300.0f;			// Additive force when shooting eggs
	float characterBounce = 400.0f;	// The force when colliding with other characters

	public GameObject eggPrefab;
	Durability durability;
	PowerUpCollector powerups;

	public float maxFiniteFlight = 3.0f;
	float finiteFlight = 0.0f;
	public float moveSpeed = 7.0f;
	public float jumpSpeed = 5.0f;
	public Vector2 throwSpeed = new Vector2(5.0f, -4.0f);
	public int playerNumber = 1;
	bool facingRight;

	// Use this for initialization
	void Start() {
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		facingRight = spriteRenderer.flipX;

		powerups = GetComponent<PowerUpCollector>();
		durability = GetComponent<Durability>();
		Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer);
		eggCount = maxEggCount;
		finiteFlight = maxFiniteFlight;
	}
	
	// Update is called once per frame
	void Update() {
		
		inputVector = Vector2.zero;

		if (stunLength <= 0 && !durability.IsFinished())
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
					eggCount -= powerups.shotCost;
					MakeEgg(throwSpeed.x, throwSpeed.y);
					for (int i = 0; i < powerups.mExtraEggThrows; i++)
					{
						MakeEgg(throwSpeed.x * (1.0f + 1.1f * (Random.value * 2 - 1)), throwSpeed.y * (1.0f + 1.1f * (Random.value * 2 - 1)));
					}
					
					// push player using recoil on shot
					rb.AddForce(new Vector2(-0.5f * (facingRight ? 1 : -1), 0.5f) * recoil);

					eggWait = eggFireInterval;
				}
			}
			else
			{
				// Decrease Egg Production (regen) when low on eggs
				eggCount += Time.deltaTime * (((int)eggCount) / maxEggCount * (1 - eggRegen) + eggRegen);
			}
		} else
		{
			stunLength -= Time.deltaTime;
		}

		inputVector.Normalize();
		eggWait -= Time.deltaTime;
		if (eggCount > maxEggCount)
			eggCount = maxEggCount;
	}

	void MakeEgg(float xSpeed, float ySpeed) {
		
		// Create the game object for an egg
		GameObject egg = (GameObject)Instantiate(eggPrefab, transform.position, Quaternion.identity);
		egg.layer = gameObject.layer;
		Rigidbody2D eggrb = egg.GetComponent<Rigidbody2D>();
		egg.transform.localScale *= powerups.sizeChange;

		egg.GetComponent<EggBehavior>().attackPower *= powerups.attackChange;

		eggrb.gravityScale *= powerups.gravityChange;

		if (powerups.mExtraEggThrows <= 0 && Input.GetAxis("Vertical" + playerNumber) < 0)
		{
			// Set special anti-gravity egg
			eggrb.gravityScale *= -0.1f;
			eggrb.velocity = new Vector2(xSpeed * (facingRight ? 1 : -1) * 0.1f, ySpeed * 3.2f);
			egg.GetComponent<EggBehavior>().bounceCount = 2;
		}
		else
		{
			// Set the velocity of the egg, add bonus velocity based on player velocity
			eggrb.velocity = new Vector2(xSpeed * (facingRight ? 1 : -1) - rb.velocity.x * 0.4f, ySpeed - Mathf.Abs(rb.velocity.x) * 0.5f);
			eggrb.velocity *= powerups.speedChange;

			if (Mathf.Abs(rb.velocity.x) < 0.2f)
			{
				// if player has small or no x-velocity, add more bounce to egg
				egg.GetComponent<EggBehavior>().bounceCount += 2;
			}
		}
		eggrb.angularVelocity = (facingRight ? -1 : 1) * Random.value * 360.0f;
	}

	void FixedUpdate() {
		// apply friction to x-axis
//		rb.velocity = new Vector2(inputVector.x * moveSpeed, rb.velocity.y);
		rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(inputVector.x * moveSpeed * powerups.moveChange, rb.velocity.y), 0.2f);

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

			float yDiff = transform.position.y - cd.collider.transform.position.y;
			if (yDiff > 0.2f)
			{
				eggCount += 0.6f;
				finiteFlight += 0.2f;
			} else if (yDiff < -0.2f)
			{
				stunLength = 0.3f;
				eggCount -= 0.6f;
				finiteFlight -= 0.2f;
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
