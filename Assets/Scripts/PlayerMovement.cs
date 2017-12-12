using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            beetBoi.velocity = new Vector2(moveX * playerTurboSpeed, Mathf.Clamp(beetBoi.velocity.y, maxFallSpeed, Mathf.Infinity));
        }
        else
        {
            if (wallSliding)
            {
                beetBoi.velocity = new Vector2(moveX * playerSpeed > 5f || moveX * playerSpeed < -5f ? moveX * playerSpeed : 0f, Mathf.Clamp(beetBoi.velocity.y, -5f, 30f));
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
        
    }

    void Jump()
    {
        if (beetBoi.velocity.y == 0)
        {
            beetBoi.AddForce(wallSliding ? Vector2.up * playerJumpPower * 1.5f : Vector2.up * playerJumpPower);
        }
        else if (beetBoi.velocity.y != 0 && wallSliding)
        {
            Debug.Log("WALLJUMP");
            wallSliding = false;
            beetBoi.AddForce(new Vector2(-2000, 2000));
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
        if (collision.collider.tag == "Floor")
        {
            ContactPoint2D contact = collision.contacts[0];

            if (contact.normal == new Vector2(1, 0) || contact.normal == new Vector2(-1, 0))
            {
                Debug.Log("SLIDING");
                wallSliding = true;
            }
        }




    }

    private void OnCollisionExit2D (Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {
            wallSliding = false;
        }
    }

}
