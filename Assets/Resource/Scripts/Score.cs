using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // <-- 1. 必須匯入場景管理

public class Score : MonoBehaviour
{
    public static Score Instance;
    public int currentScore = 0;
    public TextMeshProUGUI scoreText;

    // --- 2. 【新增】Boss 場景的設定 ---
    [Header("Boss 場景設定")]
    public int scoreToTriggerBoss = 200;  // 觸發 Boss 戰的分數
    public string bossSceneName = "Boss"; // Boss 關卡的場景名稱
    private bool bossTriggered = false; // 用來確保只觸發一次
    // ---

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 檢查是否有指定 scoreText，避免錯誤
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString();
        }
    }

    // --- 3. 【修改】這個函式 ---
    public void AddScore(int pointsToAdd)
    {
        // 如果 Boss 已經被觸發了，就不用再加分或檢查了
        if (bossTriggered) return;

        currentScore += pointsToAdd;

        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString();
        }

        // --- 4. 【新增】檢查分數是否達標 ---
        if (currentScore >= scoreToTriggerBoss && !bossTriggered)
        {
            TriggerBossScene();
        }
    }

    // --- 5. 【新增】切換場景的函式 ---
    void TriggerBossScene()
    {
        Debug.Log("分數到達 " + scoreToTriggerBoss + "! 準備進入 Boss 關卡!");
        bossTriggered = true; // 標記為已觸發，防止重複載入
        SceneManager.LoadScene(bossSceneName); // 載入 Boss 場景
    }
}