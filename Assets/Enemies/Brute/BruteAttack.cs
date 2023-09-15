using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteAttack : MonoBehaviour
{
	private Animator animator;
	private EnemyMovement enemyMovement;
	private GameObject player;
	public Transform laserSpawnPoint;
	public GameObject laserPrefab;
	public AudioSource laserSound;

	// The time since the last shot
	private float timer;
	// The time between shots
	public float shootInterval = 1.5f;

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

		// If the player is within the 1f, start kicking
		if (playerDirection.magnitude < 1f)
		{
			animator.SetBool("isAttacking", true);
		}
		else
		{
			animator.SetBool("isAttacking", false);
		}

		// If the player is within the attack range and more than 1f away, start shooting
		if (playerDirection.magnitude < enemyMovement.attackRange && playerDirection.magnitude > 1f)
		{
			// Add the time since Update was last called to the timer
			timer += Time.deltaTime;
			// If the timer exceeds the shoot interval, shoot
			if (timer > shootInterval)
			{
				timer = 0;
				// Play the shoot animation
				animator.SetTrigger("Shoot");

				// Instantiate the laser prefab at the spawn point
				Instantiate(laserPrefab, laserSpawnPoint.position, laserSpawnPoint.rotation);

				// Play the laser sound
				laserSound.Play();
			}
		}
	}
}

