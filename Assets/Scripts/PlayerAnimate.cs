using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimate : MonoBehaviour
{

	public UnityEngine.Animator animator;

	private Rigidbody2D body;

	// Use this for initialization
	void Start ()
	{
        body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        animator.SetFloat("xSpeed", Mathf.Abs(body.velocity.x));

        animator.SetFloat("ySpeed", body.velocity.y);

    }
}
