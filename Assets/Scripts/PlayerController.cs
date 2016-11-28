using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	Rigidbody2D rb;
	SpriteRenderer spriteRenderer;
	Vector2 inputVector;

	public float eggFireInterval = 0.3f;
	public float maxEggCount = 30.0f;
	public float eggCount = 0.0f;
	float eggWait;
	float recoil = 300.0f;

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

		// Set vertical velocity to jump
		if (Input.GetAxis("Vertical" + playerNumber) > 0 & finiteFlight > 0)	{
			finiteFlight = Mathf.Max(finiteFlight - Time.deltaTime, 0);
			rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
		}

		// Set inputVector left and right on input
		inputVector += new Vector2(Input.GetAxis("Horizontal" + playerNumber), 0.0f);

		// Produce an egg
		if (Input.GetAxis("Fire" + playerNumber) > 0) {
			if (eggCount >= 1 && eggWait <= 0 && (durability != null && !durability.IsDead()))
			{
				eggCount -= 1;
				MakeEgg();
				eggWait = eggFireInterval;
			}
		} else
		{
			eggCount = Mathf.Min(eggCount + Time.deltaTime, maxEggCount);
		}

		inputVector.Normalize();
		eggWait -= Time.deltaTime;
	}

	void MakeEgg() {
		
		// Create the game object for an egg
		GameObject egg = (GameObject)Instantiate(eggPrefab, transform.position, Quaternion.identity);
		egg.layer = gameObject.layer;
		Rigidbody2D eggrb = egg.GetComponent<Rigidbody2D> (); 

		// Set the velocity of the egg
		eggrb.velocity = new Vector2(throwSpeed.x * (facingRight ? 1 : -1) - rb.velocity.x * 0.4f, throwSpeed.y - Mathf.Abs(rb.velocity.x) * 0.5f);
		eggrb.angularVelocity = (facingRight ? -1 : 1) * 360;
		if (Mathf.Abs(rb.velocity.x) < 0.2f)
		{
			egg.GetComponent<EggBehavior>().bounceCount += 2;
		}

		rb.AddForce(new Vector2(-0.5f * (facingRight ? 1 : -1), 0.5f) * recoil);
	}

	void FixedUpdate() {
		
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
		if (cd.collider.CompareTag("Wall") && rb.velocity.y <= 0)
		{
			finiteFlight = maxFiniteFlight;
		}
	}
}
