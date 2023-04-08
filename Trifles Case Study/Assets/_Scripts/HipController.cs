using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipController : MonoBehaviour
{
    public float swipeSpeed = 0.5f;
    public float minHipHeight = 2f;
    public float maxHipHeight = 5f;

    private Vector3 startPos;
    private Rigidbody rb;
    private bool isMoving;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            Vector3 movement = new Vector3(startPos.x, startPos.y, transform.position.z) - transform.position;
            rb.MovePosition(transform.position + movement * swipeSpeed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            float swipeMagnitude = Mathf.Clamp(rb.velocity.magnitude, 0f, 5f);
            Vector3 direction = collision.contacts[0].normal;
            rb.AddForce(direction * swipeMagnitude * 100f);
        }
    }

    private void OnMouseDown()
    {
        isMoving = true;
        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPos.z = transform.position.z;
    }

    private void OnMouseDrag()
    {
        Vector3 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPos.z = transform.position.z;
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(currentPos.y, minHipHeight, maxHipHeight), transform.position.z);
    }

    private void OnMouseUp()
    {
        isMoving = false;
    }
}

