using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int playerHealth = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Damage"))
        {
            --playerHealth;
            if (playerHealth <= 0)
            {
                // Todo: death and hit animations
                Destroy(gameObject);
            }
        }
    }
}
