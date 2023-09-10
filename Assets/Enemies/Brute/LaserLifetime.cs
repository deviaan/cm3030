using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLifetime : MonoBehaviour
{
	public float lifetime = 1f;

	// Update is called once per frame
	void Update()
	{
		lifetime -= Time.deltaTime;
		if (lifetime < 0)
		{
			Destroy(gameObject);
		}
	}
}
