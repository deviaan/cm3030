using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntAttack : MonoBehaviour
{
	private Animator animator;
	private EnemyMovement enemyMovement;
	private GameObject player;

	void Start()
	{
		// Get the components
		animator = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player");
		enemyMovement = GetComponent<EnemyMovement>();
	}

	void Update()
	{
		// Get the distance between the player and the enemy
		Vector3 playerDirection = player.transform.position - transform.position;

		// If the player is within the attack range, start attacking
		if (playerDirection.magnitude < enemyMovement.attackRange)
		{
			animator.SetBool("isAttacking", true);
		}
		else
		{
			animator.SetBool("isAttacking", false);
		}
	}
}
