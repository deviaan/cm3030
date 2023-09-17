using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	// Enemy health as number of hits by a banana
	public int hitPoints = 3;
	private Animator animator;

	void Start()
	{
		// Get the animator component
		animator = GetComponent<Animator>();
	}

	void OnTriggerEnter2D(Collider2D hitInfo)
	{
		// If hit by a banana, take 1 damage
		if (hitInfo.name == "Banana(Clone)")
		{
			hitPoints--;

			// If 0 or less, enemy dies
			if (hitPoints == 0)
			{
				// Play death animation which calls Death() at the end
				animator.SetTrigger("Death");

				// Disable enemy movement and freeze on death
				GetComponent<Rigidbody2D>().velocity = Vector3.zero;
				GetComponent<EnemyMovement>().enabled = false;

				// Disable enemy attack on death (if applicable)
				GetComponent<BruteAttack>().enabled = false;
				GetComponent<TurretAttack>().enabled = false;
				GetComponent<GruntAttack>().enabled = false;

				// Disable collider on death
				this.enabled = false;
			}
			// If more than 0, enemy hit
			else if (hitPoints > 0)
			{
				// Play hit animation
				animator.SetTrigger("Hit");
			}
		}
	}

	// Called at the end of the death animation to destroy the enemy
	void Death()
	{
		Destroy(gameObject);
	}
}
