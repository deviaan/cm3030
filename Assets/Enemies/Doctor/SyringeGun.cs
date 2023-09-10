using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeGun : MonoBehaviour
{
	public Transform syringeSpawnPoint;
	private GameObject player;
	float timer;
	public float shootInterval = 1.5f;
	private Animator animator;
	private EnemyMovement enemyMovement;

	public GameObject syringePrefab;

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
			timer += Time.deltaTime;
			if (timer > shootInterval)
			{
				timer = 0;
				Shoot();
			}
		}
	}

	void Shoot()
	{
		animator.SetTrigger("Shoot");
		Instantiate(syringePrefab, syringeSpawnPoint.position, syringeSpawnPoint.rotation);
	}
}
