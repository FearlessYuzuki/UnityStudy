using UnityEngine;
using System.Collections;
using TMPro; // 必须引用这个命名空间来控制 TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TextMeshProUGUI scoreText;
    private int currentScore = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    // 提供给外部调用的加分方法
    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreUI();
        StopAllCoroutines();
        StartCoroutine(ScorePopEffect());
    }
    IEnumerator ScorePopEffect() {
        float duration = 0.1f;
        Vector3 originalScale = Vector3.one;
        Vector3 targetScale = Vector3.one * 1.2f; // 放大 1.2 倍

        // 快速放大
        scoreText.transform.localScale = targetScale;
        yield return new WaitForSeconds(duration);
        // 回弹
        scoreText.transform.localScale = originalScale;
    }

    // 刷新显示
    void UpdateScoreUI()
    {
        scoreText.text = "SCORE  " + currentScore.ToString("D6");
    }
}