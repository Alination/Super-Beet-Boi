using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    public int playerSpeed = 10;
    public int playerTurboSpeed = 20;
    public int playerJumpPower = 1250;
    public int shortJumpStrength = 10;
    public int maxFallSpeed = -20;
    public float wallFriction = 1.5f;

    private Rigidbody2D beetBoi;
    private float moveX;
    private bool facingRight = true;
    private bool wallSliding = false;

    public AudioClip jumpClip;
    public AudioClip deathClip;

    private AudioSource jumpSource;
    private AudioSource deathSource;

    public float wallNormal = -1f;
    public bool IsSliding
    {
        get
        {
            return this.wallSliding;
        }
    }


    // Use this for initialization
    void Start () {
        beetBoi = gameObject.GetComponent<Rigidbody2D>();

        jumpSource = gameObject.AddComponent<AudioSource>();
        jumpSource.clip = jumpClip;
        jumpSource.playOnAwake = false;

        deathSource = gameObject.AddComponent<AudioSource>();
        deathSource.clip = deathClip;
        deathSource.playOnAwake = false;
    }
	
	// Update is called once per frame
	void Update () {
        PlayerMove();
    }

    void PlayerMove()
    {
        //Controls
        moveX = Input.GetAxis("Horizontal");

        if (Input.GetButton("Turbo") || Input.GetAxis("Triggers") == 1 || Input.GetAxis("Triggers") == -1)
        {
            beetBoi.velocity = new Vector2(moveX * playerTurboSpeed, wallSliding ? Mathf.Clamp(beetBoi.velocity.y, maxFallSpeed, Mathf.Infinity) : Mathf.Clamp(beetBoi.velocity.y, maxFallSpeed, Mathf.Infinity));
        }
        else if (moveX < 0 || moveX > 0)
        {
            if (wallSliding)
            {
                beetBoi.velocity = new Vector2(moveX * playerSpeed > 5f || moveX * playerSpeed < -5f ? moveX * playerSpeed : 0f, Mathf.Clamp(beetBoi.velocity.y, -15f, 30f));
            }
            else
            {
                beetBoi.velocity = new Vector2(moveX * playerSpeed, Mathf.Clamp(beetBoi.velocity.y, maxFallSpeed, Mathf.Infinity));
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        //Beet Boi jumps lower if the button is pressed shortly
        if (beetBoi.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            beetBoi.velocity += Vector2.up * Physics2D.gravity.y * shortJumpStrength * Time.deltaTime;
        }

        //Player Direction
        if (moveX < 0.0f && facingRight == true)
        {
            FlipPlayer();
        }
        else if (moveX > 0.0f && facingRight == false)
        {
            FlipPlayer();
        }

        //Did it fall to its death?
        if (beetBoi.transform.position.y < -11)
        {
            StartCoroutine("Died");
        }
        
    }

    void Jump()
    {
        jumpSource.Play();
        if (beetBoi.velocity.y == 0)
        {
            beetBoi.AddForce(wallSliding ? Vector2.up * playerJumpPower * 1.5f : Vector2.up * playerJumpPower);
        }
        else if (beetBoi.velocity.y != 0 && wallSliding)
        {
            beetBoi.AddForce(new Vector2(3000f * wallNormal, 2000f));
            wallSliding = false;
        }

    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 scale = gameObject.transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            ContactPoint2D contact = collision.contacts[0];
            Debug.Log("COLIIIDE: " + contact.normal.x);

            if (contact.normal.x > 0.9f || contact.normal.x < -0.9f)
            {
                Debug.Log("SLIIIDE");
                wallSliding = true;
                wallNormal = contact.normal.x;
            }
        }
    }

    private void OnCollisionExit2D (Collision2D collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            wallSliding = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sawblade"))
        {
            StartCoroutine("Died");
        }
        else if (collision.CompareTag("Girl"))
        {
            StartCoroutine("Win");
        }
    }

    IEnumerator Died()
    {
        deathSource.Play();
        Debug.Log(wallSliding);
        beetBoi.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Level 1");
    }

    IEnumerator Win()
    {
        beetBoi.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Main Menu");
    }

}
