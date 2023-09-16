using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : DetectionZone
{
    public string DoorOpenAnimatorParamName = "DoorOpen";

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (detectedObjs.Count > 0)
        {
            animator.SetBool(DoorOpenAnimatorParamName, true);
        } else
        {
            animator.SetBool(DoorOpenAnimatorParamName, false);
        }
    }
}
