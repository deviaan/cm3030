using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	public float speed = 2.5f;
	public GameObject pointA;
	public GameObject pointB;
	private Transform target;
	public GameObject player;
	private Rigidbody rb;

	public float attackRange = 1.5f;
	public float visibilityRange = 10f;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		// Set the target to pointA
		target = pointB.transform;
	}

	// Update is called once per frame
	void Update()
	{
		// Get the distance between the player and the enemy
		Vector3 playerDirection = player.transform.position - transform.position;
		// If the enemy is close enough to the player, follow the player

		if (playerDirection.magnitude < attackRange) {
			// TODO: Enable attack
			rb.velocity = Vector3.zero;
		}
		else if (playerDirection.magnitude < visibilityRange)
		{
			// Move the enemy towards the player
			transform.LookAt(player.transform);

			rb.velocity = transform.forward * (speed * 1.5f);
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
			transform.LookAt(target);

			rb.velocity = transform.forward * speed;
		}


	}
}
