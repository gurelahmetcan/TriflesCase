using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    #region Fields
    
    // Takla atacak obje
    public GameObject cylinder;

    // Swipe minimum uzunluğu ve swipe hassasiyeti
    public float minSwipeLength = 50f;
    public float swipeSensitivity = 0.2f;

    // Swipe pozisyonları
    private Vector2 startPos;
    private Vector2 endPos;

    // Takla animasyonu için gereken değişkenler
    private bool isFlipping = false;
    private Quaternion flipTargetRotation;
    private float flipRotationSpeed = 800f;

    // Cylinder'ın hareketi için gereken değişkenler
    private bool isMoving = false;
    private Vector3 moveTargetPosition;
    private float moveSpeed = 5f;

    // Takla animasyonu için gereken değişkenler
    private float flipHeight = 2f;
    private float flipDuration = 1f;
    private float flipStartTime;
    
    private int count = 0;
    
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
        // Swipe kontrolü
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startPos = Input.GetTouch(0).position;
        }

        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endPos = Input.GetTouch(0).position;
            count++;
            Debug.Log(count);

            // Swipe mesafesi
            float swipeLength = (endPos - startPos).magnitude;

            // Swipe yönü
            Vector2 swipeDirection = (endPos - startPos).normalized;

            // Swipe kontrolü
            if (swipeLength > minSwipeLength && swipeDirection.y > -swipeSensitivity && swipeDirection.y < swipeSensitivity)
            {
                // Cylinder'ın konumunu hesapla ve hareketini başlat
                if (!isMoving && !isFlipping)
                {
                    Vector3 moveDirection = new Vector3(swipeDirection.x, 0f, 0f);
                    moveTargetPosition = cylinder.transform.position + moveDirection;
                    isMoving = true;

                    // Takla animasyonu
                    flipTargetRotation = cylinder.transform.rotation * Quaternion.Euler(0f, 0f, swipeDirection.x < 0 ? -180f : 180f);
                    isFlipping = true;
                    flipStartTime = Time.time;
                }
            }
        }
        
        // Takla animasyonu kontrolü
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

        // Cylinder'ın hareket kontrolü
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

