using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    #region Fields
    
    public TextMeshProUGUI timerText;
    public float timeRemaining = 90f;
    
    #endregion

    #region Unity Methods
    
    void Update()
    {
        RunTimer();
    }

    #endregion

    #region Private Methods

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

    #endregion
}
