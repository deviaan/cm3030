using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteAttack : MonoBehaviour
{
	private Animator animator;
	private EnemyMovement enemyMovement;
	private GameObject player;
	private float timer;
	public float shootInterval = 1.5f;
	public Transform laserSpawnPoint;
	public GameObject laserPrefab;

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

		if (playerDirection.magnitude < 1f)
		{
			animator.SetBool("isAttacking", true);
		}
		else
		{
			animator.SetBool("isAttacking", false);
		}

		if (playerDirection.magnitude < enemyMovement.attackRange && playerDirection.magnitude > 1f)
		{
			timer += Time.deltaTime;
			if (timer > shootInterval)
			{
				timer = 0;
				animator.SetTrigger("Shoot");
				Instantiate(laserPrefab, laserSpawnPoint.position, laserSpawnPoint.rotation);
			}
		}
	}
}

