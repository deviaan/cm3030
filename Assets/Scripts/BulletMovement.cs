using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float initialForce = 30f;
    public Rigidbody2D rb;
    void Start()
    {
        var force = new Vector2(initialForce, 0);
        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
