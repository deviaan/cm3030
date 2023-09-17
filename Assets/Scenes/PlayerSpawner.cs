using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] public GameObject player;
    private void Start()
    {
        var transform1 = transform;
        Instantiate(player, transform1.position, transform1.rotation);
    }
}
