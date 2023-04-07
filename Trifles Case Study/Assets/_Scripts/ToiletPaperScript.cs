using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletPaperScript : MonoBehaviour
{
    public float forceMultiplier = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rb.AddForce(direction * forceMultiplier);
    }
}
