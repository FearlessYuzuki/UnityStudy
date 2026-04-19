using UnityEngine;
using TMPro; // 控制 TextMeshPro 必须用到

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
        // 使用 Mathf.CeilToInt 向上取整，这样即使是 0.1 秒也会显示 1，直到真正变为 0
        int seconds = Mathf.CeilToInt(timeRemaining);
        timerText.text = "TIME: " + seconds.ToString();
    }

    void EndGame()
    {
        gameEnded = true;
        Debug.Log("倒计时结束！");
        
        // 【核心魔法】将游戏时间流逝速度设为 0
        Time.timeScale = 0f; 
        
        // 建议：如果你做了一个包含“重新开始”按钮的 UI 面板，可以在这里把它激活
        // gameOverPanel.SetActive(true); 
    }
}