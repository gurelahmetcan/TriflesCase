using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBall : MonoBehaviour
{
    public Vector3 startForce;
    
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(startForce, ForceMode.Impulse);
    }

    private void Update()
    {
        if (gameObject.transform.localPosition.y is < -2 or > 6)
        {
            gameObject.transform.localPosition = new Vector3(0f, 2.5f, 0f);
        }
    }
}
