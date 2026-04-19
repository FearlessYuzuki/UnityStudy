using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI 引用")]
    public TextMeshProUGUI timerText;

    [Header("游戏设置")]
    public float timeRemaining = 60f; // 默认60秒倒计时
    private bool gameEnded = false;

    void Update()
    {
        // 如果游戏已经结束，就不再往下执行了
        if (gameEnded) return;

        // 如果时间还没扣完
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime; // 减去上一帧所花费的时间
            UpdateTimerUI();
        }
        else
        {
            // 时间归零，触发结束
            timeRemaining = 0;
            UpdateTimerUI();
            EndGame();
        }
    }

    void UpdateTimerUI()
    {
       
        int seconds = Mathf.CeilToInt(timeRemaining);
        timerText.text = "TIME: " + seconds.ToString();
    }

    void EndGame()
    {
        gameEnded = true;
        Debug.Log("倒计时结束！");
        
        Time.timeScale = 0f; 
        
    }
}