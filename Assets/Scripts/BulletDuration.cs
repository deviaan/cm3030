using System;
using UnityEngine;

public class BulletDuration : MonoBehaviour
{
    [SerializeField] private float bulletDuration = 1.0f;
    void Start()
    {
        Destroy(gameObject, bulletDuration);
    }
}
