using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    #region Fields
    
    public GameObject cylinder;

    public float minSwipeLength = 50f;
    public float swipeSensitivity = 0.2f;

    private Vector2 startPos;
    private Vector2 endPos;

    private bool isFlipping = false;
    private Quaternion flipTargetRotation;
    private float flipRotationSpeed = 800f;

    private bool isMoving = false;
    private Vector3 moveTargetPosition;
    private float moveSpeed = 5f;

    private float flipHeight = 2f;
    private float flipDuration = 1f;
    private float flipStartTime;
    
    #endregion

    #region Unity Methods
    
    private void Update()
    {
        ToilerPaperSwipe();
    }

    #endregion

    #region Private Methods
    
    private void ToilerPaperSwipe()
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

            if (swipeLength > minSwipeLength && swipeDirection.y > -swipeSensitivity && swipeDirection.y < swipeSensitivity)
            {
                if (!isMoving && !isFlipping)
                {
                    Vector3 moveDirection = new Vector3(swipeDirection.x, 0f, 0f);
                    moveTargetPosition = cylinder.transform.position + moveDirection;
                    isMoving = true;

                    flipTargetRotation = cylinder.transform.rotation * Quaternion.Euler(0f, 0f, swipeDirection.x < 0 ? -180f : 180f);
                    isFlipping = true;
                    flipStartTime = Time.time;
                }
            }
        }
        
        if (isFlipping)
        {
            float t = (Time.time - flipStartTime) / flipDuration;
            float height = Mathf.Sin(Mathf.PI * t) * flipHeight;

            cylinder.transform.rotation = Quaternion.RotateTowards(cylinder.transform.rotation, flipTargetRotation, flipRotationSpeed * Time.deltaTime);
            cylinder.transform.position = new Vector3(cylinder.transform.position.x, height, cylinder.transform.position.z);

            if (cylinder.transform.rotation == flipTargetRotation)
            {
                isFlipping = false;
            }
        }

        if (isMoving)
        {
            cylinder.transform.position = Vector3.MoveTowards(cylinder.transform.position, moveTargetPosition, moveSpeed * Time.deltaTime);

            if (cylinder.transform.position == moveTargetPosition)
            {
                isMoving = false;
            }
        }
    }

    #endregion

}

