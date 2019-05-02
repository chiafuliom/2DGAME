using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	private Rigidbody2D myRigidbody;

	public Animator myAnimator;

	public float movementSpeed;

	private bool facingRight;

	[SerializeField]
	private Transform[] groundPoints; //creates something to collide with the ground

	[SerializeField]
	private float groundRadius;//creates the size of the colliders

	[SerializeField]
	private LayerMask whatIsGround;//defines what is ground

	private bool isGrounded;//can be set to true or false based on our position

	private bool jump;//can be set to true or false when we press the space key

	[SerializeField]
	private float jumpForce;//allows us to determine the magnitude of the jump

	public bool isAlive;

	public GameObject reset;

	private Slider healthBar;

	public float health = 10f;

	private float healthBurn = 5f;

	// Use this for initialization
	void Start () {

		myRigidbody = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();
		facingRight = true;
		isAlive = true;
		reset.gameObject.SetActive (false);
		healthBar = GameObject.Find ("health slider").GetComponent<Slider> ();
		healthBar.minValue = 0f;
		healthBar.maxValue = health;
		healthBar.value = healthBar.maxValue;
	}
	
	// Update is called once per frame
	void Update () {
		HandleInput ();
	}

	void FixedUpdate(){
		float horizontal = Input.GetAxis ("Horizontal");
		//Debug.Log (horizontal);
		if (isAlive) {
			HandleMovement (horizontal);
			Flip (horizontal);
		}else{
			return;
			}
		isGrounded = IsGrounded ();

	}

	//function definitions
	private void HandleMovement(float horizontal){
		if(isGrounded && jump){
			isGrounded = false;
			jump = false;
			myRigidbody.AddForce (new Vector2 (0,jumpForce));
		}
		myRigidbody.velocity = new Vector2 (horizontal * movementSpeed, myRigidbody.velocity.y);
		myAnimator.SetFloat ("speed", Mathf.Abs (horizontal));
	}

	private void Flip(float horizontal){
		if(horizontal<0 && facingRight  || horizontal>0 && !facingRight){
			facingRight = !facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}

	}

	private void HandleInput(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			jump = true;
			//Debug.Log ("I'm jumping");
			myAnimator.SetBool("jumping", true);
		}
	}

	private bool IsGrounded(){
		if (myRigidbody.velocity.y <= 0) {
			//if player is not moving vertically, test each of Player's groundPoints for collision with Ground
			foreach (Transform point in groundPoints) {
				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position, groundRadius, whatIsGround);
				for (int i = 0; 1 < colliders.Length; i++) {
					if (colliders [i].gameObject != gameObject) {
						return true;

					}
				}
			}
		}
		return false;
	}

	void UpdateHealth(){
		if (health > 0) {
			health -= healthBurn;
			healthBar.value = health;
		}
		if (health <= 0) {
			ImDead ();
		}
	}

  public  void AddHealth()
    {
        if (health > -1)
        {
            health += healthBurn;
            healthBar.value = health;
            Debug.Log("Health Added");
        }
    }

	public void ImDead(){
		isAlive = false;
		myAnimator.SetBool ("dead", true);
		reset.gameObject.SetActive (true);
	}

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "health")
        {
            AddHealth();
        }
    }
    void OnCollisionEnter2D(Collision2D target){
		if (target.gameObject.tag == "Ground") {
			myAnimator.SetBool ("jumping", false);
		}
		if (target.gameObject.tag == "deadly") {
			ImDead();
		}
		if (target.gameObject.tag == "damage") {
			UpdateHealth();
		}
        if (target.gameObject.tag == "health")
        {
            AddHealth();
        }
    }
}
