using UnityEngine;
using TMPro;

public class ScoreUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI scoreText; // UI untuk menampilkan skor
    public TextMeshProUGUI feedbackText; // UI untuk menampilkan feedback
    public TextMeshProUGUI moneyText; // UI untuk menampilkan uang

    public ScoreManager scoreManager; // Referensi ke ScoreManager

    // Threshold untuk feedback
    private int poorScoreThreshold = 20;
    private int goodScoreThreshold = 50;
    private int greatScoreThreshold = 100;

    private void Start()
    {
        // Cek apakah ada skor dan uang yang tersimpan, jika ada, tampilkan
        if (PlayerPrefs.HasKey("PlayerScore") && PlayerPrefs.HasKey("PlayerMoney"))
        {
            int savedScore = PlayerPrefs.GetInt("PlayerScore");
            int savedMoney = PlayerPrefs.GetInt("PlayerMoney");

            // Set skor dan uang di ScoreManager berdasarkan data yang disimpan
            scoreManager.SetScore(savedScore);
            scoreManager.SetMoney(savedMoney);

            UpdateUI(); // Tampilkan UI dengan data yang sudah di-load
        }
        else
        {
            // Jika tidak ada data tersimpan, tampilkan UI dengan nilai awal
            UpdateUI();
        }
    }

    // Update UI berdasarkan data dari ScoreManager
    public void UpdateUI()
    {
        UpdateScoreUI();
        UpdateMoneyUI();
        GiveFeedback(scoreManager.GetScore());
    }

    // Update skor di UI
    private void UpdateScoreUI()
    {
        int score = scoreManager.GetScore();
        if (scoreText != null)
        {
            scoreText.text = $"Score    : {score}";
        }
    }

    // Update uang di UI
    private void UpdateMoneyUI()
    {
        int money = scoreManager.GetMoney();
        if (moneyText != null)
        {
            moneyText.text = $"Reward    : $ {money}";
        }
    }

    // Menampilkan feedback di UI berdasarkan skor
    private void GiveFeedback(int currentScore)
    {
        if (currentScore >= greatScoreThreshold)
        {
            feedbackText.text = "Feedback   : Sangat Bagus!";
            Debug.Log("Feedback: Sangat Bagus!");
        }
        else if (currentScore >= goodScoreThreshold)
        {
            feedbackText.text = "Feedback   : Bagus!";
            Debug.Log("Feedback: Bagus!");
        }
        else if (currentScore >= poorScoreThreshold)
        {
            feedbackText.text = "Feedback   : Kurang Bagus";
            Debug.Log("Feedback: Kurang Bagus");
        }
        else
        {
            feedbackText.text = "Feedback   : Tidak Bagus";
        }
    }
}
