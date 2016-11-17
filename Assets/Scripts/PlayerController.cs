using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	Rigidbody2D rigidBody;
	SpriteRenderer spriteRenderer;
	Vector2 inputVector;
	PhysicsMaterial2D bounceMat;

	public GameObject eggPrefab;

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

		bounceMat = Resources.Load<PhysicsMaterial2D>("bounce");
		Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer);
	}
	
	// Update is called once per frame
	void Update() {
		
		inputVector = Vector2.zero;

		// Set vertical velocity to jump
		if (playerNumber == 1 && Input.GetKeyDown(KeyCode.W) ||	playerNumber == 2 && Input.GetKeyDown(KeyCode.UpArrow))	{
			rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
		}

		// Set inputVector left and right on input
		if (playerNumber == 1 && Input.GetKey(KeyCode.A) || playerNumber == 2 && Input.GetKey(KeyCode.LeftArrow)) {
			inputVector += new Vector2(-1.0f, 0.0f);
		}

		if (playerNumber == 1 && Input.GetKey(KeyCode.D) ||	playerNumber == 2 && Input.GetKey(KeyCode.RightArrow))	{
			inputVector += new Vector2(1.0f, 0.0f);
		}

		// Produce an egg
		if (playerNumber == 1 && Input.GetKeyDown(KeyCode.S) ||	playerNumber == 2 && Input.GetKeyDown(KeyCode.DownArrow)) {
			MakeEgg();
		}

		inputVector.Normalize();
	}

	void MakeEgg(){
		
		// Create the game object for an egg
		GameObject egg = (GameObject)Instantiate(eggPrefab, transform.position, Quaternion.identity);
		egg.layer = gameObject.layer;
		Rigidbody2D eggRigidBody = egg.GetComponent<Rigidbody2D> (); 
		// Set the velocity of the egg
		eggRigidBody.velocity = new Vector2(throwSpeed.x * (facingRight ? 1 : -1), throwSpeed.y);
		eggRigidBody.angularVelocity = (facingRight ? -1 : 1) * 360;
	}

	void FixedUpdate(){
		
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
