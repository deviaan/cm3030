using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 5f;

	// Start is called before the first frame update
	void Start()
	{
		// Initialize the rigidbody
		Rigidbody rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		// Get the input from the player
		float horizontal = Input.GetAxis("Horizontal");

		// Move the player
		Vector3 direction = new Vector3(horizontal, 0, 0);
		direction = direction.normalized;
		transform.Translate(direction * speed * Time.deltaTime, Space.World);
	}
}
