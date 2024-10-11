using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
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

    [Header("PowerUp Data")]
    [SerializeField] private float sprintMultiplier = .2f;
    [SerializeField] private float sprintDuration = 5f;
    private float currentSprintMultiplier = 1f;
    private bool isSprinting;
    private ContinuousMoveProviderBase moveProvider;
    private Coroutine sprintCoroutine;

    [Header("Timer Settings")]
    private float taskDuration; // Waktu tugas dalam detik yang diterima dari TaskManager
    private float currentTime;

    private void Start()
    {
        moveProvider = FindObjectOfType<ContinuousMoveProviderBase>();

        UpdateBudgetUI(); // Inisialisasi tampilan budget di UI
        UpdateMoneyUI();  // Inisialisasi tampilan uang di UI
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
                powerUpCost = 50; // Harga power-up Sprint
                if (playerMoney >= powerUpCost)
                {
                    playerMoney -= powerUpCost;
                    AddSprintPowerUp();
                    Debug.Log("Purchased Sprint power-up.");
                }
                else
                {
                    Debug.Log("Not enough money to purchase Sprint power-up.");
                }
                break;

            case "BudgetBooster":
                powerUpCost = 100; // Harga Budget Booster
                if (playerMoney >= powerUpCost)
                {
                    playerMoney -= powerUpCost;
                    playerBudget += 100; // Tambahkan budget sebagai efek dari Budget Booster
                    UpdateBudgetUI();
                    Debug.Log("Purchased Budget Booster power-up.");
                }
                else
                {
                    Debug.Log("Not enough money to purchase Budget Booster.");
                }
                break;

            case "TimeExtension":
                powerUpCost = 150; // Harga Time Extension
                if (playerMoney >= powerUpCost)
                {
                    playerMoney -= powerUpCost;
                    currentTime += 120; // Tambahkan 60 detik ke timer
                    Debug.Log("Purchased Time Extension power-up.");
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
            budgetText.text = $"Budget: $ {playerBudget:N0}"; // Tampilkan budget sebagai angka tanpa simbol mata uang
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
            moneyText.text = $"Money    : $ {playerMoney:N0}"; // Update uang yang ada di UI
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
