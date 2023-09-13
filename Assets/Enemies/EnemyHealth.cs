using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public int hitPoints = 3;
	private Animator animator;

	void OnTriggerEnter2D(Collider2D hitInfo)
	{
		if (hitInfo.name == "Banana(Clone)")
		{
			hitPoints--;
			if (hitPoints <= 0)
			{
				animator.SetTrigger("Death");
			}
			else
			{
				animator.SetTrigger("Hit");
			}
		}
	}

	void Death()
	{
		Destroy(gameObject);
	}
}
