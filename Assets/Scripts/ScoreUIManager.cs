using UnityEngine;
using TMPro;

public class ScoreUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI scoreText; // UI untuk menampilkan skor
    public TextMeshProUGUI feedbackText; // UI untuk menampilkan feedback
    public TextMeshProUGUI moneyText; // UI untuk menampilkan uang

    // Referensi untuk bintang UI
    public GameObject star0; // Bintang 0
    public GameObject star1; // Bintang 1
    public GameObject star2; // Bintang 2
    public GameObject star3; // Bintang 3
    public GameObject star4; // Bintang 4
    public GameObject star5; // Bintang 5

    public ScoreManager scoreManager; // Referensi ke ScoreManager

    // Threshold untuk feedback
    private int poorScoreThreshold = 50;
    private int goodScoreThreshold = 100;
    private int greatScoreThreshold = 150;
    private int exellentScoreThreshold = 200;
    private int amazingScoreThreshold = 250;

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

        UpdateStarsUI(scoreManager.GetScore());
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
            moneyText.text = $"{money}";
            // moneyText.text = $"Reward    : $ {money}";
        }
    }

     private void UpdateStarsUI(int score)
    {
        // Nonaktifkan semua bintang terlebih dahulu
        star0.SetActive(false);
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
        star4.SetActive(false);
        star5.SetActive(false);

        // Aktifkan bintang yang sesuai berdasarkan skor
        if (score < poorScoreThreshold)
        {
            star0.SetActive(true); // Bintang 0
        }
        else if (score < goodScoreThreshold)
        {
            star1.SetActive(true); // Bintang 1
        }
        else if (score < greatScoreThreshold)
        {
            star2.SetActive(true); // Bintang 2
        }
        else if (score < exellentScoreThreshold)
        {
            star3.SetActive(true); // Bintang 3
        }
        else if (score < amazingScoreThreshold)
        {
            star4.SetActive(true); // Bintang 4
        }
        else
        {
            star5.SetActive(true); // Bintang 5
        }
    }
}
