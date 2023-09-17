using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syringe : MonoBehaviour
{
	private Rigidbody2D rb;

	// Speed of the syringe
	public float speed = 5f;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();

		// Move the syringe in the direction it is facing
		rb.velocity = transform.right * speed;
	}

	void OnTriggerEnter2D(Collider2D hitInfo)
	{
		// Destroy the syringe on collision
		Destroy(gameObject);
	}
}
