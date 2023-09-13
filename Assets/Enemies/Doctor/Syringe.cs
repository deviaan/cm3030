using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syringe : MonoBehaviour
{
	public float speed = 5f;
	private Rigidbody2D rb;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = transform.right * speed;
	}

	void OnTriggerEnter2D(Collider2D hitInfo)
	{
		Debug.Log(hitInfo.name);
		Destroy(gameObject);
	}
}
