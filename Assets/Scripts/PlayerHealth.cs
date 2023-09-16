using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int playerHealth = 5;

    public void HitPlayer()
    {
        --playerHealth;
        if (playerHealth <= 0)
        {
            // Todo: death and hit animations
            Destroy(gameObject);
        }
    }
}
