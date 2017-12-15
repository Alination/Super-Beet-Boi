using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMainMenu : MonoBehaviour
{
    public int playerJumpPower = 1250;

    private Rigidbody2D beetBoi;


    // Use this for initialization
    void Start()
    {
        beetBoi = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        //Controls

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
            StartCoroutine("startGame");

        }

    }

    void Jump()
    {
        if (beetBoi.velocity.y == 0)
        {
            beetBoi.AddForce(Vector2.up * playerJumpPower);
        }

    }

    IEnumerator startGame()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Level 1");
    }

}
