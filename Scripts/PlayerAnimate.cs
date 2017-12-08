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
		this.body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.animator.SetFloat("MoveSpeed", Mathf.Abs(this.body.velocity.x));
	}
}
