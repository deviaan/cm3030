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

		// If the enemy is close enough to the player, follow the player
		if (playerDirection.magnitude < attackRange)
		{
			// TODO: Enable attack
			Move(Vector3.zero);
		}
		else if (playerDirection.magnitude < visibilityRange)
		{
			// Move the enemy towards the player
			Move(playerDirection);
		}
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

			Move(direction);
		}
	}

	private void Move(Vector3 direction)
	{
		// Move the enemy
		rb.velocity = new Vector2(direction.normalized.x * speed, rb.velocity.y);

		// Flip enemy based on direction
		if (direction.x < 0)
		{
			transform.localScale = new Vector3(-1, 1, 1);
		}
		else if (direction.x > 0)
		{
			transform.localScale = new Vector3(1, 1, 1);
		}

	}
}
