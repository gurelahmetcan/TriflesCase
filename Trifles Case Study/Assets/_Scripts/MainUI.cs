using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainUI : MonoBehaviour
{
    #region Fields
    
    [SerializeField] private TextMeshProUGUI m_TimerText;
    [SerializeField] private TextMeshProUGUI m_LevelSuccessText;
    [SerializeField] private float timeRemaining = 90f;

    [SerializeField] private GameObject ToiletSwipe;
    [SerializeField] private GameObject BouncingBall;

    private bool m_IsBouncingBall;
    private float m_ConstantTime = 90f;

    private bool m_TweenStarted;
    private Sequence m_LevelSuccessSequence;
    
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
        m_TimerText.text = timeString;

        if (timeRemaining <= 0 && !m_TweenStarted)
        {
            // Stop the timer when time runs out
            timeRemaining = 0;
            m_TimerText.gameObject.SetActive(false);
            m_TweenStarted = true;

            if (m_LevelSuccessSequence != null)
            {
                m_LevelSuccessSequence = null;
                m_LevelSuccessSequence.Kill();
            }

            m_LevelSuccessSequence = DOTween.Sequence();
            
            m_LevelSuccessText.gameObject.SetActive(true);
            m_LevelSuccessSequence
                .Append(m_LevelSuccessText.gameObject.GetComponent<RectTransform>().DOScale(1.2f, 0.5f)
                    .SetLoops(3, LoopType.Yoyo)).Append(m_LevelSuccessText.DOFade(0f, 0.5f)).OnComplete((
                    () =>
                    {
                        m_IsBouncingBall = !m_IsBouncingBall;
                        BouncingBall.SetActive(m_IsBouncingBall);
                        ToiletSwipe.SetActive(!m_IsBouncingBall);
                        timeRemaining = m_ConstantTime;
                        m_TimerText.gameObject.SetActive(true);
                        m_LevelSuccessText.gameObject.SetActive(false);
                        m_LevelSuccessText.DOFade(1f, 0.1f);
                        m_TweenStarted = false;
                    }));
        }
    }

    #endregion
}
