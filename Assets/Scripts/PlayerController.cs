using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	Rigidbody2D rigidBody;
	SpriteRenderer spriteRenderer;
	Vector2 inputVector;

	public float eggFireInterval = 0.3f;
	float eggWait;
	float recoil = 300.0f;

	public GameObject eggPrefab;
	Durability durability;

	public float moveSpeed = 5.0f;
	public float jumpSpeed = 8.0f;
	public Vector2 throwSpeed = new Vector2(4.0f, 5.0f);
	public int playerNumber = 1;
	bool facingRight;

	// Use this for initialization
	void Start() {
		rigidBody = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		facingRight = spriteRenderer.flipX;

		durability = GetComponent<Durability>();
		Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer);
	}
	
	// Update is called once per frame
	void Update() {
		
		inputVector = Vector2.zero;

		// Set vertical velocity to jump
		if (Input.GetAxis("Vertical" + playerNumber) > 0)	{
			rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
		}

		// Set inputVector left and right on input
		inputVector += new Vector2(Input.GetAxis("Horizontal" + playerNumber), 0.0f);

		// Produce an egg
		if (Input.GetAxis("Fire" + playerNumber) > 0) {
			if (eggWait <= 0 && (durability != null && !durability.IsDead()))
			{
				MakeEgg();
				eggWait = eggFireInterval;
			}
		}

		inputVector.Normalize();
		eggWait -= Time.deltaTime;
	}

	void MakeEgg() {
		
		// Create the game object for an egg
		GameObject egg = (GameObject)Instantiate(eggPrefab, transform.position, Quaternion.identity);
		egg.layer = gameObject.layer;
		Rigidbody2D eggRigidBody = egg.GetComponent<Rigidbody2D> (); 

		// Set the velocity of the egg
		eggRigidBody.velocity = new Vector2(throwSpeed.x * (facingRight ? 1 : -1) - rigidBody.velocity.x * 0.4f, throwSpeed.y - Mathf.Abs(rigidBody.velocity.x) * 0.5f);
		eggRigidBody.angularVelocity = (facingRight ? -1 : 1) * 360;
		if (Mathf.Abs(rigidBody.velocity.x) < 0.2f)
		{
			egg.GetComponent<EggBehavior>().bounceCount += 2;
		}

		rigidBody.AddForce(new Vector2(-0.5f * (facingRight ? 1 : -1), 0.5f) * recoil);
	}

	void FixedUpdate() {
		
		rigidBody.velocity = new Vector2(inputVector.x * moveSpeed, rigidBody.velocity.y);

		if (rigidBody.velocity.x < 0) {
			facingRight = true;
		} 
		else if (rigidBody.velocity.x > 0) {
			facingRight = false;
		}

		spriteRenderer.flipX = facingRight;
	}
}
