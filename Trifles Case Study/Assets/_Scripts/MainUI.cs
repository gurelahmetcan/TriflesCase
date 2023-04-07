using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timeRemaining = 90f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RunTimer();
    }

    private void RunTimer()
    {
        timeRemaining -= Time.deltaTime;

        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);

        // Display the time in the format "01:30"
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = timeString;

        if (timeRemaining <= 0)
        {
            // Stop the timer when time runs out
            timeRemaining = 0;
            Debug.Log("Time's up!");
        }
    }
}
