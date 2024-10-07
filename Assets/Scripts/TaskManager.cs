using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI taskDescriptionText; // UI untuk menampilkan deskripsi task
    public Button acceptButton;
    public Button rejectButton;

    [Header("References")]
    public ShopManager shopManager; // Referensi ke ShopManager untuk mengatur budget player

    [Header("Task Data")]
    public Task currentTask;

    private void Start()
    {
        GenerateInitialTask();
        GenerateNewTask();
        acceptButton.onClick.AddListener(AcceptTask);
        rejectButton.onClick.AddListener(RejectTask);
    }

    private void GenerateInitialTask()
    {
        currentTask = new Task(); // Membuat task baru dengan nilai acak
        currentTask.Initialize();
        Debug.Log("Initial Task: " + currentTask.room + " with style: " + currentTask.style + " and budget: " + currentTask.budget);
    }

    private void GenerateNewTask()
    {
        currentTask = new Task(); // Membuat task baru
        currentTask.Initialize();  // Menginisialisasi task dengan nilai acak
        Debug.Log("New Task: " + currentTask.room + " with style: " + currentTask.style + " and budget: " + currentTask.budget);

        // Update UI setelah membuat task
        FindObjectOfType<TaskUIManager>().UpdateTaskUI();
    }

    // Accept task
    public void AcceptTask()
    {
        // Pindahkan player ke building mode
        EnterBuildingMode();

        // Setel budget ke ShopManager
        shopManager.SetPlayerBudget(currentTask.budget);

        // Setel waktu task ke ShopManager
        shopManager.SetTaskTime(currentTask.time);
    }

    // Reject task
    public void RejectTask()
    {
        GenerateNewTask();

        // Mengaktifkan kembali tombol
        acceptButton.interactable = true;
        rejectButton.interactable = true;
    }

    // Method untuk memulai building mode
    private void EnterBuildingMode()
    {
        shopManager.OpenShop();
    }
}
