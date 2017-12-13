using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinBlade : MonoBehaviour {


	#region Fields - Public

	public float rotationsPerSecond = 2.0f;

	#endregion

	new private Renderer renderer; 

        // Use this for initialization
	void Start () {
		this.renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		this.renderer.transform.RotateAround (this.renderer.transform.position, new Vector3 (0, 0, 1), this.rotationsPerSecond * 1000 * UnityEngine.Time.deltaTime);
	}
}
