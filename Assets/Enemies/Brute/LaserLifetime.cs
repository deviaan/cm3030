using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLifetime : MonoBehaviour
{
	// How long the laser will last
	public float lifetime = 0.2f;

	void Update()
	{
		// Destroy the laser after the lifetime
		lifetime -= Time.deltaTime;
		if (lifetime < 0)
		{
			Destroy(gameObject);
		}
	}
}
