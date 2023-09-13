using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    [SerializeField] private float seconds = 5.0f;
    
    void Start()
    {
        Destroy(gameObject, seconds);
    }
}
