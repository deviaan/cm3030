using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float force = 10f;
    public Rigidbody2D rb;
    void Start()
    {
        rb.velocity = new Vector2(force, 0);
    }
}
