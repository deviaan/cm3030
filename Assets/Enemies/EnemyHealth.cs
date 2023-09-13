using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public int hitPoints = 3;

	void OnTriggerEnter2D(Collider2D hitInfo)
	{
		if (hitInfo.name == "Banana(Clone)")
		{
			hitPoints--;
			if (hitPoints <= 0)
			{
				Destroy(gameObject);
			}
		}
	}
}
