using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hip : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 endPos;
    
    public float minSwipeLength = 50f;
    public float swipeSensitivity = 0.2f;
    
    void Update()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startPos = Input.GetTouch(0).position;
        }
        else if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            var currentPos = Mathf.Lerp(2f, 5f, Input.GetTouch(0).position.normalized.y);
            transform.position = new Vector3(0f, currentPos, 0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                endPos = Input.GetTouch(0).position;
                
                float swipeLength = (endPos - startPos).magnitude;
            
                Vector2 swipeDirection = (endPos - startPos).normalized;
            
                if (swipeLength > minSwipeLength)
                {
                    Vector3 moveDirection = new Vector3(swipeDirection.x, swipeDirection.y, 0f);
                    collision.rigidbody.AddForce(-moveDirection * (swipeLength / 15) * swipeSensitivity, ForceMode.Impulse);
                }
            }
        }
    }
}
