using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
	private Animator animator;
	private EnemyMovement enemyMovement;
	private GameObject player;

	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player");
		enemyMovement = GetComponent<EnemyMovement>();
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 playerDirection = player.transform.position - transform.position;

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
