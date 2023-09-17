using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttack : MonoBehaviour
{
	public Transform syringeSpawnPoint;
	private GameObject player;
	private Animator animator;
	private EnemyMovement enemyMovement;

	// The time since the last shot
	float timer;
	// The time between shots
	public float shootInterval = 1.5f;
	// The syringe prefab
	public GameObject syringePrefab;

	void Start()
	{
		// Get the components
		animator = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player");
		enemyMovement = GetComponent<EnemyMovement>();
	}

	void Update()
	{
		if (!player)
		{
			player = GameObject.FindGameObjectWithTag("Player");
		}
		else
		{
			// Get the distance between the player and the enemy
			Vector3 playerDirection = player.transform.position - transform.position;
			
			// If the player is within the attack range, start attacking
			if (Mathf.Abs(playerDirection.y) < 3 && Mathf.Abs(playerDirection.x) < enemyMovement.attackRange)
			{
				// Add the time since Update was last called to the timer
				timer += Time.deltaTime;

				// If the timer exceeds the shoot interval, shoot
				if (timer > shootInterval)
				{
					timer = 0;
					Shoot();
				}
			}
		}

		void Shoot()
		{
			// Play the shoot animation
			animator.SetTrigger("Shoot");

			// Instantiate the syringe prefab at the spawn point
			Instantiate(syringePrefab, syringeSpawnPoint.position, syringeSpawnPoint.rotation);
		}
	}
}
