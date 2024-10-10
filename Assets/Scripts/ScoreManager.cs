using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score = 0; // Skor pemain
    private int money = 0; // Uang yang diperoleh pemain

    // Thresholds
    private int poorScoreThreshold = 20;
    private int goodScoreThreshold = 50;
    private int greatScoreThreshold = 100;

    // Reward Values
    public int rewardPoor = 5;
    public int rewardGood = 10;
    public int rewardGreat = 20;

    private void Start() 
    {
        
        LoadScoreAndMoney();    
    }

    // Menambah skor
    public void AddScore(int value)
    {
        score += value;
        Debug.Log($"Score added: {value}. Current score: {score}");

        GiveReward(); // Memberikan reward setelah skor ditambahkan

        SaveScoreAndMoney();
    }

    // Reset skor dan uang
    public void ResetScoreAndMoney()
    {
        score = 0;
        money = 0;
        SaveScoreAndMoney();
    }

    // Memberikan reward berdasarkan skor saat ini
    private void GiveReward()
    {
        if (score >= greatScoreThreshold)
        {
            money = rewardGreat;
            Debug.Log($"Reward given: {rewardGreat}. Current money: {money}");
        }
        else if (score >= goodScoreThreshold)
        {
            money = rewardGood;
            Debug.Log($"Reward given: {rewardGood}. Current money: {money}");
        }
        else if (score >= poorScoreThreshold)
        {
            money = rewardPoor;
            Debug.Log($"Reward given: {rewardPoor}. Current money: {money}");
        }
    }

    // Simpan skor dan uang ke PlayerPrefs sebelum pindah scene
    public void SaveScoreAndMoney()
    {
        PlayerPrefs.SetInt("PlayerScore", score);
        PlayerPrefs.SetInt("PlayerMoney", money);
        PlayerPrefs.Save();
        Debug.Log($"Score and money saved: {score}, {money}");
    }

    public void LoadScoreAndMoney()
    {
        score = PlayerPrefs.GetInt("PlayerScore", 0);
        money = PlayerPrefs.GetInt("PlayerMoney", 0);

        Debug.Log($"Score and money loaded: {score}, {money}");
    }

    // Fungsi untuk mendapatkan skor dan uang dari luar
    public int GetScore()
    {
        return score;
    }

    public int GetMoney()
    {
        return money;
    }

    // Fungsi untuk mengatur skor dari luar (GameManager)
    public void SetScore(int newScore)
    {
        score = newScore;
        SaveScoreAndMoney();
    }

    // Fungsi untuk mengatur uang dari luar (GameManager)
    public void SetMoney(int newMoney)
    {
        money = newMoney;
        SaveScoreAndMoney();
    }
}
