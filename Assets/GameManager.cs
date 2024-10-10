using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("References")]
    public ShopManager shopManager;
    public ScoreManager scoreManager; // Referensi ke ScoreManager

    // Data untuk menyimpan budget, style, room, time
    [Header("Data")]
    public int playerBudget;
    public string playerStyle;
    public string playerRoom;
    public float playerTime;
    public int playerMoney;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Agar GameManager tetap ada saat scene berubah
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        // Berlangganan ke event SceneManager ketika scene berubah
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Menghapus langganan dari event SceneManager
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Callback yang dipanggil saat scene selesai dimuat
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Mencari ShopManager dan ScoreManager di scene baru jika ada
        shopManager = FindObjectOfType<ShopManager>();
        scoreManager = FindObjectOfType<ScoreManager>();

        if (shopManager != null)
        {
            Debug.Log("ShopManager ditemukan di scene baru.");
            // Set budget dan data lainnya ke ShopManager
            shopManager.SetPlayerBudget(playerBudget);
            shopManager.SetTaskTime(playerTime);
            shopManager.SetPlayerMoney(playerMoney);
        }
        else
        {
            Debug.LogWarning("ShopManager tidak ditemukan di scene.");
        }

        if (scoreManager != null)
        {
            Debug.Log("ScoreManager ditemukan di scene baru.");
            // Muat skor dan uang ke ScoreManager
            scoreManager.LoadScoreAndMoney(); // Memuat skor dan uang saat scene dimuat
        }
    }

    // Metode untuk menyimpan data task (budget, style, room, time, money)
    public void SaveGameData(int budget, string style, string room, float time, int money)
    {
        playerBudget = budget;
        playerStyle = style;
        playerRoom = room;
        playerTime = time;
        playerMoney = money;

        // if (scoreManager != null)
        // {
        //     scoreManager.SaveScoreAndMoney();
        // }
    }

    public void AddMoney(int amount)
    {
        playerMoney += amount; // Tambahkan uang
    }

    public void DeductMoney(int amount)
    {
        if (playerMoney >= amount)
        {
            playerMoney -= amount; // Kurangi uang
        }
        else
        {
            Debug.LogWarning("Not enough money!");
        }
    }

    public int GetPlayerMoney()
    {
        return playerMoney;
    }

    public void OnTaskDone()
    {
        if (scoreManager != null)
        {
            scoreManager.ResetScoreAndMoney(); // Reset skor, feedback, dan uang
            Debug.Log("Score, feedback, and money have been reset.");
        }
        else
        {
            Debug.LogWarning("ScoreManager not found!");
        }
    }

    // Metode untuk menyimpan skor dan uang ke PlayerPrefs

    // Metode untuk memulai loading scene shop (bisa dipanggil dari skrip lain)
    public void LoadShopScene()
    {
        StartCoroutine(LoadSceneAsync("ShopScene")); // Ganti "ShopScene" dengan nama scene shop
    }

    // Coroutine untuk memuat scene secara asynchronous
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive); // Load additive

        while (!asyncLoad.isDone)
        {
            yield return null; // Tunggu hingga scene selesai dimuat
        }

        // Setelah scene selesai dimuat, cari ShopManager
        shopManager = FindObjectOfType<ShopManager>();

        if (shopManager != null)
        {
            Debug.Log("ShopManager ditemukan setelah scene dimuat.");
            shopManager.SetPlayerBudget(playerBudget);
            shopManager.SetTaskTime(playerTime);
        }
    }

    public void BuyItemFromShop(ItemData item)
    {
        if (shopManager != null)
        {
            shopManager.SelectItem(item); // Memilih item di shop
            shopManager.BuySelectedItem(); // Membeli item tersebut
        }
        else
        {
            Debug.LogWarning("ShopManager belum diatur atau tidak ditemukan!");
        }
    }
}
