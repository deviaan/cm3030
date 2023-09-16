using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int playerHealth = 5;
    [SerializeField] public Animator animator;
    [SerializeField] public float hitStun = 1.0f;
    private bool _isDead = false;
    public bool wasHit = false;
    public float hitStunClear;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Damage") && !_isDead && !wasHit)
        {
            --playerHealth;
            
            if (playerHealth <= 0)
            {
                _isDead = true;
            }
            else
            {
                wasHit = true;
                animator.SetBool("IsHit", true);
                hitStunClear = Time.time + hitStun;
            }
        }
    }

    public void ClearHitStun()
    {
        wasHit = false;
    }

    public bool PlayerCanAct()
    {
        return Time.time > hitStunClear;
    }
}
