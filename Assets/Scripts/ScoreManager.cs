using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI scoreText; // UI untuk menampilkan skor
    public TextMeshProUGUI feedbackText; // UI untuk menampilkan feedback
    public TextMeshProUGUI moneyText; // UI untuk menampilkan uang

    [Header("Score Data")]
    private int score = 0; // Skor pemain
    private int money = 0; // Uang yang diperoleh pemain

    // Feedback Thresholds
    private int poorScoreThreshold = 20; // Batas skor untuk feedback kurang bagus
    private int goodScoreThreshold = 50; // Batas skor untuk feedback bagus
    private int greatScoreThreshold = 100; // Batas skor untuk feedback sangat bagus

    // Reward Value
    public int rewardPoor = 5; // Reward untuk feedback "Kurang Bagus"
    public int rewardGood = 10; // Reward untuk feedback "Bagus"
    public int rewardGreat = 20; // Reward untuk feedback "Sangat Bagus"

    private void Start()
    {
        UpdateScoreUI(); // Update tampilan UI saat permainan dimulai
        UpdateMoneyUI(); // Update tampilan UI uang
    }

    // Menambah skor
    public void AddScore(int value)
    {
        score += value;
        Debug.Log($"Score added: {value}. Current score: {score}");

        // Memberikan feedback berdasarkan nilai skor saat ini
        GiveFeedback(score);

        // Memberikan reward setelah feedback diberikan
        GiveReward();

        UpdateScoreUI(); // Update UI setelah skor ditambahkan
    }

    // Memberikan feedback berdasarkan skor
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

    // Memberikan reward berdasarkan feedback
    private void GiveReward()
    {
        if (score >= greatScoreThreshold)
        {
            money = rewardGreat;
            Debug.Log($"Reward given: {rewardGreat}. Current money: {money}");
        }
        else if (score >= goodScoreThreshold)
        {
            if (score < greatScoreThreshold)
            {
                money = rewardGood;
                Debug.Log($"Reward given: {rewardGood}. Current money: {money}");
            }
        }
        else if (score >= poorScoreThreshold)
        {
            if (score < goodScoreThreshold) // Untuk skor antara 20 sampai 49
            {
                money = rewardPoor; // Tetap 5 reward
                Debug.Log($"Reward given: {rewardPoor}. Current money: {money}");
            }
        }

        UpdateMoneyUI(); // Update UI uang setelah menerima reward
    }

    // Update UI untuk menampilkan skor
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score    : {score}";
        }
    }

    // Update UI untuk menampilkan uang
    private void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = $"Reward    : $ {money}";
        }
    }
}
