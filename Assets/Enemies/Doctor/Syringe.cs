using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syringe : MonoBehaviour
{
	public float speed = 5f;
	private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

	void OnTriggerEnter(Collider hitInfo) {
		Debug.Log(hitInfo.name);
		Destroy(gameObject);
	}
}
