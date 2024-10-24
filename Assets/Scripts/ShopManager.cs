using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ShopManager : MonoBehaviour
{
    [Header("Shop Items")]
    [SerializeField] private List<ItemData> availableItems;
    [SerializeField] private GameObject[] spawnPoints; // Array untuk menyimpan GameObject yang digunakan sebagai spawn points
    private int currentSpawnIndex = 0; // Index posisi spawn saat ini

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI budgetText; // Referensi ke TextMeshPro untuk menampilkan budget di UI
    [SerializeField] private TextMeshProUGUI timerText;  // Referensi ke TextMeshPro untuk menampilkan timer di UI
    [SerializeField] private TextMeshProUGUI moneyText;  // Referensi ke TextMeshPro untuk menampilkan uang di UI

    private ItemData selectedItem;
    private float playerBudget;
    public int playerMoney;


    [Header("Gameplay Settings")]
    [SerializeField] private Transform setPointGameplay;
    [SerializeField] private GameObject UI_Gameover;
    [SerializeField] private FadeScreen fadeScreen;
    [SerializeField] private GameObject player;
    [SerializeField] private ScoreManager scoreManager;
    // [SerializeField] private GameObject endButton;

    [Header("PowerUp Data")]
    [SerializeField] private float sprintMultiplier = .2f;
    [SerializeField] private float sprintDuration = 5f;

    [SerializeField] private GameObject buySprintButton;
    [SerializeField] private GameObject buyBudgetBoosterButton;
    [SerializeField] private GameObject buyTimeExtensionButton;
    [SerializeField] private GameObject selectSprintButton;
    [SerializeField] private GameObject selectBudgetBoosterButton;
    [SerializeField] private GameObject selectTimeExtensionButton;
    private float currentSprintMultiplier = 1f;
    private bool isSprinting;
    private ContinuousMoveProviderBase moveProvider;
    private Coroutine sprintCoroutine;

    [Header("Timer Settings")]
    private float taskDuration; // Waktu tugas dalam detik yang diterima dari TaskManager
    private float currentTime;

    private bool isSprintPurchased = false;
    private bool isBudgetBoosterPurchased = false;
    private bool isTimeExtensionPurchased = false;

    private bool isSprintUsedInScene = false;
    private bool isBudgetBoosterUsedInScene = false;
    private bool isTimeExtensionUsedInScene = false;

    private void Start()
    {
        moveProvider = FindObjectOfType<ContinuousMoveProviderBase>();

        ResetPowerUpUsage();

        isSprintPurchased = PlayerPrefs.GetInt("SprintPurchased", 0) == 1;
        isBudgetBoosterPurchased = PlayerPrefs.GetInt("BudgetBoosterPurchased", 0) == 1;
        isTimeExtensionPurchased = PlayerPrefs.GetInt("TimeExtensionPurchased", 0) == 1;

        UpdateBudgetUI(); // Inisialisasi tampilan budget di UI
        UpdateMoneyUI();  // Inisialisasi tampilan uang di UI

        UpdatePowerUpButtons();
        
        // if (endButton != null)
        // {
        //     endButton.GetComponent<Button>().onClick.AddListener(OnEndButtonPressed);
        // }
    }

    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 0)
            {
                currentTime = 0; // Set ke 0 jika nilai negatif
            }
            UpdateTimerUI(); // Update tampilan timer di UI setiap frame

            if (currentTime == 0)
            {
                HandleTimeOut();
            }
        }
    }

    private void UpdatePowerUpButtons()
    {
        buySprintButton.SetActive(!isSprintPurchased);
        buyBudgetBoosterButton.SetActive(!isBudgetBoosterPurchased);
        buyTimeExtensionButton.SetActive(!isTimeExtensionPurchased);
        
        selectSprintButton.SetActive(isSprintPurchased);
        selectBudgetBoosterButton.SetActive(isBudgetBoosterPurchased);
        selectTimeExtensionButton.SetActive(isTimeExtensionPurchased);
    }

    // public void OnEndButtonPressed()
    // {
    //     // Hentikan timer
    //     currentTime = 0;
    //     UpdateTimerUI();

    //     // Tampilkan UI GameOver, pindahkan player, dan jalankan fade
    //     if (fadeScreen != null)
    //     {
    //         StartCoroutine(GameOverSequence());
    //     }
    //     else
    //     {
    //         TriggerGameOver();
    //     }
    // }

    private void HandleTimeOut()
    {
        Debug.Log("Task time has run out!");

        if (scoreManager != null)
        {
            // scoreManager.AddRewardAndResetScore();
            UpdateMoneyUI(); // Update UI setelah menambahkan reward
        }

        // Jalankan FadeOut terlebih dahulu, lalu aktifkan UI Gameover dan pindahkan player
        if (fadeScreen != null)
        {
            StartCoroutine(GameOverSequence());
        }
        else
        {
            TriggerGameOver();
        }
    }

    private IEnumerator GameOverSequence()
    {
        fadeScreen.FadeOut();
        // Tunggu hingga fade selesai (sesuai dengan durasi fade)
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        TriggerGameOver();

        fadeScreen.FadeIn();
    }

    private void TriggerGameOver()
    {
        // Pindahkan player ke posisi SetPointGameplay
        if (setPointGameplay != null && player != null)
        {
            player.transform.position = setPointGameplay.position;
            Debug.Log($"Player moved to SetPointGameplay at position: {setPointGameplay.position}");
        }

        // Aktifkan UI Gameover
        if (UI_Gameover != null)
        {
            UI_Gameover.SetActive(true);
            Debug.Log("Game over UI activated.");
        }
    }

    // Membuka shop dan menampilkan item-item yang tersedia
    public void AddMoney(int moneyToAdd)
    {
        scoreManager.SetMoney(scoreManager.GetMoney() + moneyToAdd); // Tambahkan uang ke skor ShopManager
        UpdateMoneyUI(); // Update UI setelah menambahkan uang
        Debug.Log($"Added {moneyToAdd} to shop. New total money: {scoreManager.GetMoney()}");
    }
    public void SetPlayerMoney(int money)
    {
        playerMoney = money;
        UpdateMoneyUI(); // Update UI untuk menampilkan uang terbaru
    }

    // Set budget yang diterima dari TaskManager
    public void SetPlayerBudget(float budget)
    {
        playerBudget = budget;
        Debug.Log($"Player Budget set to: {playerBudget}");
        UpdateBudgetUI(); // Update UI saat budget diatur
    }
    public float GetPlayerBudget()
    {
        return playerBudget;
    }

    // Set waktu tugas yang diterima dari TaskManager
    public void SetTaskTime(float time)
    {
        taskDuration = time;
        currentTime = taskDuration;
        UpdateTimerUI(); // Update UI saat waktu diatur
    }

    // Memilih item untuk dibeli
    public void SelectItem(ItemData item)
    {
        selectedItem = item;
        Debug.Log($"Selected Item: {item.itemName}");
    }

    // Membeli item yang dipilih
    public void BuySelectedItem()
    {
        if (selectedItem != null && playerBudget >= selectedItem.price)
        {
            playerBudget -= selectedItem.price;
            Debug.Log($"Purchased {selectedItem.itemName} for {selectedItem.price}. Remaining budget: {playerBudget}");

            // Instansiasi item di dunia pada posisi berikutnya
            InstantiateItem(selectedItem);
            UpdateBudgetUI(); // Update UI setelah pembelian
        }
        else
        {
            Debug.Log("Not enough budget or no item selected.");
        }
    }

    public void BuyPowerUp(string powerUpType)
    {
        int powerUpCost = 0;

        switch (powerUpType)
        {
            case "Sprint":
                if (!isSprintPurchased )
                {
                    powerUpCost = 50; // Harga power-up Sprint
                    if (playerMoney >= powerUpCost)
                    {
                        playerMoney -= powerUpCost;
                        isSprintPurchased = true;
                        PlayerPrefs.SetInt("SprintPurchased", 1);
                        // AddSprintPowerUp();
                        selectSprintButton.SetActive(true);
                        buySprintButton.SetActive(false);
                        Debug.Log("Purchased Sprint power-up.");
                    }
                    else
                    {
                        Debug.Log("Not enough money to purchase Sprint power-up.");
                    }
                }
                else
                {
                    Debug.Log("Sprint power-up already purchased.");
                }
                break;

            case "BudgetBooster":
                if (!isBudgetBoosterPurchased)
                {
                    powerUpCost = 100; // Harga Budget Booster
                    if (playerMoney >= powerUpCost)
                    {
                        playerMoney -= powerUpCost;
                        isBudgetBoosterPurchased = true;
                        PlayerPrefs.SetInt("BudgetBoosterPurchased", 1);
                        // playerBudget += 100; // Tambahkan budget sebagai efek dari Budget Booster
                        UpdateBudgetUI();
                        selectBudgetBoosterButton.SetActive(true);
                        buyBudgetBoosterButton.SetActive(false);
                        Debug.Log("Purchased Budget Booster power-up.");
                    }
                    else
                    {
                        Debug.Log("Not enough money to purchase Budget Booster.");
                    }
                }
                else
                {
                    Debug.Log("Budget Booster power-up already purchased.");
                }
                break;

            case "TimeExtension":
                if (!isTimeExtensionPurchased)
                {
                    powerUpCost = 150; // Harga Time Extension
                    if (playerMoney >= powerUpCost)
                    {
                        playerMoney -= powerUpCost;
                        isTimeExtensionPurchased = true; // Set Time Extension sebagai dibeli
                        PlayerPrefs.SetInt("TimeExtensionPurchased", 1);
                        // currentTime += 120; // Tambahkan 60 detik ke timer
                        UpdateTimerUI();
                        selectTimeExtensionButton.SetActive(true);
                        buyTimeExtensionButton.SetActive(false);
                        Debug.Log("Purchased Time Extension power-up.");
                    }
                    else
                    {
                        Debug.Log("Not enough money to purchase Time Extension.");
                    }
                }
                else
                {
                    Debug.Log("Not enough money to purchase Time Extension.");
                }
                break;

            default:
                Debug.Log("Invalid power-up type.");
                break;
        }

        UpdateMoneyUI(); // Update UI setelah membeli power-up
    }

    public void SelectPowerUp(string powerUpType)
    {
        switch (powerUpType)
        {
            case "Sprint":
                if (isSprintPurchased && !isSprintUsedInScene)
                {
                    AddSprintPowerUp();
                    isSprintUsedInScene = true;
                    // selectSprintButton.SetActive(false); // Sembunyikan tombol setelah dipilih
                    Debug.Log("Sprint power-up activated.");
                }
                break;

            case "BudgetBooster":
                if (isBudgetBoosterPurchased && !isBudgetBoosterUsedInScene)
                {
                    playerBudget += 100; // Tambahkan efek budget booster
                    UpdateBudgetUI();
                    isBudgetBoosterUsedInScene = true;
                    // selectBudgetBoosterButton.SetActive(false); // Sembunyikan tombol setelah dipilih
                    Debug.Log("Budget Booster power-up activated.");
                }
                break;

            case "TimeExtension":
                if (isTimeExtensionPurchased && !isTimeExtensionUsedInScene)
                {
                    currentTime += 120; // Tambahkan 120 detik ke timer
                    UpdateTimerUI();
                    isTimeExtensionUsedInScene = true;
                    // selectTimeExtensionButton.SetActive(false); // Sembunyikan tombol setelah dipilih
                    Debug.Log("Time Extension power-up activated.");
                }
                break;

            default:
                Debug.Log("Invalid power-up type.");
                break;
        }
    }

    private void AddSprintPowerUp()
    {
        currentSprintMultiplier += sprintMultiplier;

        if (sprintCoroutine != null)
        {
            StopCoroutine(sprintCoroutine);
        }

        sprintCoroutine = StartCoroutine(Sprint());
    }

    private IEnumerator Sprint()
    {
        isSprinting = true;
        moveProvider.moveSpeed *= currentSprintMultiplier; // Gandakan kecepatan
        Debug.Log("Sprint multiplier: " + currentSprintMultiplier);
        
        yield return new WaitForSeconds(sprintDuration); // Tunggu selama durasi sprint
        
        moveProvider.moveSpeed /= currentSprintMultiplier; // Kembalikan kecepatan ke nilai semula
        currentSprintMultiplier = 1f; // Reset multiplier setelah durasi selesai
        isSprinting = false;

        Debug.Log("Sprint power-up ended.");
    }

    public void ResetPowerUpStatus()
    {
        PlayerPrefs.SetInt("SprintPurchased", 0);
        PlayerPrefs.SetInt("BudgetBoosterPurchased", 0);
        PlayerPrefs.SetInt("TimeExtensionPurchased", 0);

        // Pastikan juga variabel lokalnya di-reset
        isSprintPurchased = false;
        isBudgetBoosterPurchased = false;
        isTimeExtensionPurchased = false;
    }

    public void ResetPowerUpUsage()
    {
        isSprintUsedInScene = false;
        isBudgetBoosterUsedInScene = false;
        isTimeExtensionUsedInScene = false;
        Debug.Log("Power-up usage reset for new scene.");
    }

    // Menginstansiasi item di posisi spawn yang telah ditentukan
    private void InstantiateItem(ItemData item)
    {
        if (item.itemPrefab == null)
        {
            Debug.Log("No item prefab found.");
            return;
        }

        if (currentSpawnIndex < spawnPoints.Length)
        {
            // Ambil posisi dari GameObject spawn point
            Vector3 spawnPosition = spawnPoints[currentSpawnIndex].transform.position;
            Instantiate(item.itemPrefab, spawnPosition, Quaternion.identity);
            Debug.Log($"{item.itemName} instantiated in the world at {spawnPosition}.");

            // Perbarui index spawn ke posisi berikutnya
            currentSpawnIndex++;

            // Pastikan kita tidak melebihi jumlah spawn points
            if (currentSpawnIndex >= spawnPoints.Length)
            {
                Debug.Log("All spawn points have been used.");
                currentSpawnIndex = 0; // Reset ke 0 jika ingin menggunakan kembali dari awal
            }
        }
        else
        {
            Debug.Log("No more available spawn points.");
        }
    }

    // Update UI untuk menampilkan player budget
    private void UpdateBudgetUI()
    {
        if (budgetText != null)
        {
            budgetText.text = $"{playerBudget:N0}"; // Tampilkan budget sebagai angka tanpa simbol mata uang
        }
    }

    // Update UI untuk menampilkan timer
    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = FormatTime(currentTime); // Format waktu dalam format MM:SS
        }
    }

    // Update UI untuk menampilkan uang yang dimiliki pemain
    private void UpdateMoneyUI()
    {
        if (moneyText != null && scoreManager != null)
        {
            moneyText.text = $"{playerMoney:N0}"; // Update uang yang ada di UI
        }
    }

    // Method untuk memformat waktu dari detik menjadi MM:SS
    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds); // Format menjadi MM:SS
    }
}
