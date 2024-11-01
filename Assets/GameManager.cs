using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // [Header("Player Spawn")]
    // public GameObject player1; // Player untuk New Game
    // public GameObject player2; // Player untuk Continue Game

    [Header("Player Prefab")]
    public GameObject player;

    [Header("Spawn Points")]
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject tutorialUI;

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

        spawn1 = GameObject.Find("SpawnNewGame"); // Pastikan Player1 memiliki tag "Player1"
        spawn2 = GameObject.Find("SpawnContinue");
        tutorialUI = GameObject.Find("UI_Tutorial");
        player = GameObject.Find("Player");


        if (scene.name == "Gameplay") // Hanya jalankan logika ini di scene "Gameplay"
        {
            SetPlayerBasedOnGameStatus();
        }

        if (shopManager != null)
        {
            Debug.Log("ShopManager ditemukan di scene baru.");
            // Set budget dan data lainnya ke ShopManager
            shopManager.SetPlayerBudget(playerBudget);
            shopManager.SetTaskTime(playerTime);
            shopManager.SetPlayerMoney(playerMoney);
            shopManager.SetTaskDetails(playerStyle, playerRoom);
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
        SaveGameData(0, "", "", 0, playerMoney);
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

    private void SetPlayerBasedOnGameStatus()
    {
        int isNewGame = PlayerPrefs.GetInt("IsNewGame", 1); // Default ke 1 (New Game)

        if (isNewGame == 1)
        {
            if (spawn1 != null && player != null)
            {
                player.transform.position = spawn1.transform.position;
                player.transform.rotation = spawn1.transform.rotation;
                Debug.Log("Player moved to spawn1 (New Game).");
            }

            if (tutorialUI != null)
            {
                tutorialUI.SetActive(true); // Aktifkan tutorial saat New Game
                Debug.Log("Tutorial UI activated for New Game.");
            }
        }
        else
        {
            if (spawn2 != null && player != null)
            {
                player.transform.position = spawn2.transform.position;
                player.transform.rotation = spawn2.transform.rotation;
                Debug.Log("Player moved to spawn2 (Continue Game).");
            }

            if (tutorialUI != null)
            {
                tutorialUI.SetActive(false); // Nonaktifkan tutorial saat Continue Game
                Debug.Log("Tutorial UI deactivated for Continue Game.");
            }
        }
    }

    public void StartNewGame()
    {
        PlayerPrefs.SetInt("IsNewGame", 1); // Set status ke New Game
        PlayerPrefs.Save();
    }

    public void ContinueGame()
    {
        PlayerPrefs.SetInt("IsNewGame", 0); // Set status ke Continue Game
        PlayerPrefs.Save();
    }
}
