using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Shop Items")]
    public List<ItemData> availableItems;
    public GameObject[] spawnPoints; // Array untuk menyimpan GameObject yang digunakan sebagai spawn points
    private int currentSpawnIndex = 0; // Index posisi spawn saat ini

    [Header("UI Elements")]
    public TextMeshProUGUI budgetText; // Referensi ke TextMeshPro untuk menampilkan budget di UI
    public TextMeshProUGUI timerText;  // Referensi ke TextMeshPro untuk menampilkan timer di UI

    private ItemData selectedItem;
    private float playerBudget;

    [Header("Timer Settings")]
    private float taskDuration; // Waktu tugas dalam detik yang diterima dari TaskManager
    private float currentTime;

    private void Start()
    {
        UpdateBudgetUI(); // Inisialisasi tampilan budget di UI
    }

    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerUI(); // Update tampilan timer di UI setiap frame
        }
        else if (currentTime < 0)
        {
            currentTime = 0;
            Debug.Log("Task time has run out!");
            // Lakukan aksi jika waktu habis, misalnya berikan penalti atau tampilkan notifikasi
        }
    }

    // Membuka shop dan menampilkan item-item yang tersedia
    public void OpenShop()
    {
        Debug.Log("Shop opened.");
        UpdateBudgetUI(); // Update UI saat membuka shop

        foreach (ItemData item in availableItems)
        {
            Debug.Log($"Item: {item.itemName} - Price: {item.price} - Style: {item.styleScore}");
        }
    }

    // Set budget yang diterima dari TaskManager
    public void SetPlayerBudget(float budget)
    {
        playerBudget = budget;
        Debug.Log($"Player Budget set to: {playerBudget}");
        UpdateBudgetUI(); // Update UI saat budget diatur
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

    // Method untuk memformat waktu dari detik menjadi MM:SS
    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds); // Format menjadi MM:SS
    }
}
