using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeroPhysicsFix : MonoBehaviour
{
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevents tilting and falling
    }

    void FixedUpdate()
    {
        // Keep hero upright
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}

