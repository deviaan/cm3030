using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	public float speed = 2.5f;
	public GameObject pointA;
	public GameObject pointB;
	private Transform target;
	private GameObject player;
	private Rigidbody2D rb;
	private Animator animator;

	// Attack range and visibility range
	public float attackRange = 1.5f;
	public float visibilityRange = 10f;


	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag("Player");
		// Set the target to pointA
		target = pointB.transform;
		animator.SetBool("isWalking", true);
	}

	// Update is called once per frame
	void Update()
	{
		// Get the distance between the player and the enemy
		Vector3 playerDirection = player.transform.position - transform.position;
		Vector3 newDirection = Vector3.zero;

		// If the player is within the attack range, stop moving
		if (playerDirection.magnitude < attackRange)
		{
			newDirection = Vector3.zero;
		}
		// If the player is within the visibility range, move towards the player
		else if (playerDirection.magnitude < visibilityRange)
		{
			newDirection = playerDirection;
		}
		// Otherwise, move to the target point
		else
		{
			Vector3 direction = target.position - transform.position;

			// If the enemy is close enough to the target, switch targets
			if (direction.magnitude < 0.5f)
			{
				if (target == pointA.transform)
				{
					target = pointB.transform;
				}
				else
				{
					target = pointA.transform;
				}
			}

			// Move towards the target
			newDirection = direction;
		}

		// Prevent walking out of bounds
		if (newDirection.x + transform.position.x < pointA.transform.position.x || transform.position.x + newDirection.x > pointB.transform.position.x)
		{
			Move(Vector3.zero);
		}
		else
		{
			Move(newDirection);
		}

		// Flip to view player if visible
		if (playerDirection.magnitude < visibilityRange)
		{
			// Flip enemy based on direction of player
			if (playerDirection.x < 0)
			{
				transform.localRotation = Quaternion.Euler(0, 180, 0);
			}
			else if (playerDirection.x > 0)
			{
				transform.localRotation = Quaternion.Euler(0, 0, 0);
			}
		}

	}

	// Move the enemy
	private void Move(Vector3 direction)
	{
		// Create velocity vector
		rb.velocity = new Vector2(direction.normalized.x * speed, rb.velocity.y);

		// Flip enemy based on direction and set animation
		if (direction.x < 0)
		{
			// Set the walking animation
			animator.SetBool("isWalking", true);
			transform.localRotation = Quaternion.Euler(0, 180, 0);
		}
		else if (direction.x > 0)
		{
			// Set the walking animation
			animator.SetBool("isWalking", true);
			transform.localRotation = Quaternion.Euler(0, 0, 0);
		}
		else
		{
			// Set the walking animation to false if not moving
			animator.SetBool("isWalking", false);
		}

	}
}
