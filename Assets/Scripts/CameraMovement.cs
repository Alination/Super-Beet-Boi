using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform player;
    public float smoothSpeed = 0.2f;
    public Vector3 offset;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;
	
	// Update is called once per frame
	void FixedUpdate () {
        float x = Mathf.Clamp(player.position.x, xMin, xMax);
        float y = Mathf.Clamp(player.position.y, yMin, yMax);

        Vector3 targetPosition = new Vector3(x, y) + offset;
        Vector3 smoothedPosition = Vector3.Lerp(gameObject.transform.position, targetPosition, smoothSpeed*Time.deltaTime);
        gameObject.transform.position = smoothedPosition;

    }
}
