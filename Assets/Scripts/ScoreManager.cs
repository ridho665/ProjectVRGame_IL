using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private int score = 0; // Skor pemain
    private int money = 0; // Uang yang diperoleh pemain

    // Thresholds
    private int poorScoreThreshold = 50;
    private int goodScoreThreshold = 100;
    private int greatScoreThreshold = 150;
    private int exellentScoreThreshold = 200;
    private int amazingScoreThreshold = 250;


    // Reward Values
    public int rewardPoor = 10;
    public int rewardGood = 20;
    public int rewardGreat = 30;
    public int rewardExellent = 40;
    public int rewardAmazing = 50;

    public int highStarCount = 0;
    public int lowStarCount = 0;

    public int winCondition = 5;
    public int loseCondition = 3;

    private bool hasHighStarAwarded = false; // Sudahkah pemain mendapatkan high star?
    private bool hasLowStarAwarded = false;  // Sudahkah pemain mendapatkan low star?
    private bool isGameStarted = false;

    public RectTransform scoreBarRect;

    public float maxWidth = 100f;

    private void Start() 
    {
        CheckInitialScore();
        LoadScoreAndMoney();  
         
    }

    private void Update() 
    {
        UpdateScoreBar();     
    }

    private void CheckInitialScore()
    {
        // Jika skor lebih dari 100 saat permainan dimulai, jangan tambahkan lowStarCount
        if (score > goodScoreThreshold)
        {
            isGameStarted = true; // Tandai bahwa permainan telah dimulai
        }
    }

    // Menambah skor
    public void AddScore(int value)
    {
        score += value;
        Debug.Log($"Score added: {value}. Current score: {score}");

        GiveReward(); // Memberikan reward setelah skor ditambahkan
        // CheckWinOrLose();

        SaveScoreAndMoney();
    }

    private void UpdateScoreBar()
    {
        if (scoreBarRect != null)
        {
            // Hitung lebar baru berdasarkan skor saat ini
            float newWidth = Mathf.Clamp((score / (float)amazingScoreThreshold) * maxWidth, 0, maxWidth);
            scoreBarRect.sizeDelta = new Vector2(newWidth, scoreBarRect.sizeDelta.y); // Ubah width
        }
    }

    // Reset skor dan uang
    public void ResetScoreAndMoney()
    {
        score = 0;
        money = 0;
        SaveScoreAndMoney();
        UpdateScoreBar();
    }

    private void GiveReward()
    {
        if (score >= goodScoreThreshold) // Skor di atas 100
        {
            if (score >= amazingScoreThreshold)
            {
                money = rewardAmazing;
                Debug.Log($"Reward given: {rewardAmazing}. Current money: {money}");
            }
            else if (score >= exellentScoreThreshold)
            {
                money = rewardExellent;
                Debug.Log($"Reward given: {rewardExellent}. Current money: {money}");
            }
            else if (score >= greatScoreThreshold)
            {
                money = rewardGreat;
                Debug.Log($"Reward given: {rewardGreat}. Current money: {money}");
            }
            else
            {
                money = rewardGood;
                Debug.Log($"Reward given: {rewardGood}. Current money: {money}");
            }

            if (!hasHighStarAwarded)
            {
                highStarCount++;
                hasHighStarAwarded = true; // Tandai bahwa high star sudah diberikan
                hasLowStarAwarded = false; // Reset low star jika mendapatkan high star
            } // Menambahkan ke highStarCount karena skor di atas 100
        }

        else if (score >= poorScoreThreshold && score < goodScoreThreshold) // Skor di antara 50-99
        {
            money = rewardPoor;
            Debug.Log($"Reward given: {rewardPoor}. Current money: {money}");

            // Tambahkan ke lowStarCount hanya jika belum mendapat highStarCount
            if (!hasLowStarAwarded && !hasHighStarAwarded && isGameStarted)
            {
                lowStarCount++;
                hasLowStarAwarded = true; // Tandai bahwa low star sudah diberikan
            }
        }
        else // Skor di bawah poorScoreThreshold (kurang dari 50)
        {
            Debug.Log("No reward given. Score is below 50.");
        }

        SaveScoreAndMoney(); // Simpan setelah perubahan
    }

    public void CheckWinOrLose()
    {
        if (highStarCount >= winCondition)
        {
            GameWon(); // Panggil fungsi untuk menang
        }
        else if (lowStarCount >= loseCondition)
        {
            GameLost(); // Panggil fungsi untuk kalah
        }
    }

    private void GameWon()
    {
        Debug.Log("Game Won! You've earned 3 stars 5 times.");
        // Tambahkan logika untuk menyelesaikan permainan atau memindahkan ke scene kemenangan
        // Contoh: SceneManager.LoadScene("WinScene");
    }

    // Fungsi jika game kalah
    private void GameLost()
    {
        SceneManager.LoadScene("LoseScene");
        Debug.Log("Game Lost! You've earned 2 stars or below 3 times.");
        // Tambahkan logika untuk menyelesaikan permainan atau memindahkan ke scene kekalahan
        // Contoh: SceneManager.LoadScene("LoseScene");
    }

    // Simpan skor dan uang ke PlayerPrefs sebelum pindah scene
    public void SaveScoreAndMoney()
    {
        PlayerPrefs.SetInt("PlayerScore", score);
        PlayerPrefs.SetInt("PlayerMoney", money);
        PlayerPrefs.SetInt("HighStarCount", highStarCount);
        PlayerPrefs.SetInt("LowStarCount", lowStarCount);
        PlayerPrefs.Save();
        Debug.Log($"Score and money saved: {score}, {money}");
    }

    public void LoadScoreAndMoney()
    {
        score = PlayerPrefs.GetInt("PlayerScore", 0);
        money = PlayerPrefs.GetInt("PlayerMoney", 0);
        highStarCount = PlayerPrefs.GetInt("HighStarCount", 0);
        lowStarCount = PlayerPrefs.GetInt("LowStarCount", 0);

        Debug.Log($"Score and money loaded: {score}, {money}");
        UpdateScoreBar();
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
        UpdateScoreBar();
    }

    // Fungsi untuk mengatur uang dari luar (GameManager)
    public void SetMoney(int newMoney)
    {
        money = newMoney;
        SaveScoreAndMoney();
    }

    public void OnSceneChange()
    {
        if (score < goodScoreThreshold) // Jika skor kurang dari 100
        {
            lowStarCount++;
            Debug.Log($"Low Star Count increased: {lowStarCount}");
        }

        SaveScoreAndMoney(); // Simpan setelah perubahan
    }
}
