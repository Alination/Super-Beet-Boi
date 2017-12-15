using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BondageAnimate : MonoBehaviour {

    public UnityEngine.Animator animator;

    private bool win = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("won", win);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            win = true;
        }
    }
    
}
