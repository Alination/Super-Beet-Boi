using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlade : MonoBehaviour {

    public float minX = 0f;
    public float maxX = 0f;
    public float minY = 0f;
    public float maxY = 0f;
    public float speedX = 0.2f;
    public float speedY = 0.2f;
    private Rigidbody2D blade;
    private Vector3 pos;

    // Use this for initialization
    void Start() {
        blade = gameObject.GetComponent<Rigidbody2D>();
        if (minX == 0f && maxX == 0f)
        {
            speedX = 0f;
        }
        if (minY == 0f && maxY == 0f)
        {
            speedY = 0f;
        }
    }
	
	// Update is called once per frame
	void Update () {

        pos = new Vector3(speedX, speedY, 0);

        transform.position += pos;

        if (transform.position.y > maxY || transform.position.y < minY)
        {
            speedY *= -1f;
        }
        if (transform.position.x > maxX || transform.position.x < minX)
        {
            speedX *= -1f;
        }
    }

}
