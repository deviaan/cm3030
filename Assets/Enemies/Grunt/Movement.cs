using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public float speed = 5f;
	public GameObject pointA;
	public GameObject pointB;
	private Transform target;
	public GameObject player;

	// Start is called before the first frame update
	void Start()
	{
		// Set the target to pointA
		target = pointA.transform;
	}

	// Update is called once per frame
	void Update()
	{


		// Get the distance between the player and the enemy
		Vector3 playerDirection = player.transform.position - transform.position;
		// If the enemy is close enough to the player, follow the player
		if (playerDirection.magnitude < 5f)
		{
			// Move the enemy towards the player
			Vector3 direction = playerDirection.normalized;
			transform.Translate(direction * (speed * 1.5f) * Time.deltaTime, Space.World);
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

			// Move the enemy towards the target
			direction = direction.normalized;
			transform.Translate(direction * speed * Time.deltaTime, Space.World);
		}


	}
}
