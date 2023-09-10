using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 5f;
	private Rigidbody2D rb;
	private Animator animator;

	// Start is called before the first frame update
	void Start()
	{
		// Initialize the rigidbody
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		// Get the input from the player
		float horizontal = Input.GetAxis("Horizontal");

		if (horizontal < 0)
		{
			animator.SetBool("isRunning", true);
			transform.localRotation = Quaternion.Euler(0, 180, 0);
		}
		else if (horizontal > 0)
		{
			animator.SetBool("isRunning", true);
			transform.localRotation = Quaternion.Euler(0, 0, 0);
		}
		else
		{
			animator.SetBool("isRunning", false);
		}

		rb.velocity = new Vector2(horizontal, 0f) * speed;
	}
}
