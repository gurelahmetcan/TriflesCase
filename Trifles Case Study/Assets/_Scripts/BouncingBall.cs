using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBall : MonoBehaviour
{
    // Swipe pozisyonları
    private Vector2 startPos;
    private Vector2 endPos;
    
    private Vector3 moveTargetPosition;

    private bool isMoving;

    public float minSwipeLength = 50f;
    public float swipeSensitivity = 0.2f;

    public Vector3 startForce;
    
    private float moveSpeed = 5f;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(startForce, ForceMode.Impulse);
    }

    void Update()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startPos = Input.GetTouch(0).position;
        }

        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endPos = Input.GetTouch(0).position;

            float swipeLength = (endPos - startPos).magnitude;

            Vector2 swipeDirection = (endPos - startPos).normalized;
            
            Debug.Log(swipeDirection);

            if (swipeLength > minSwipeLength)
            {
                if (!isMoving)
                {
                    Vector3 moveDirection = new Vector3(swipeDirection.x, swipeDirection.y, 0f);
                    moveTargetPosition = gameObject.transform.position + moveDirection;
                    gameObject.GetComponent<Rigidbody>().AddForce(moveTargetPosition * 5f, ForceMode.Impulse);
                    isMoving = true;
                }
            }
        }
    }

}
