using System.Collections;
using System.Collections.Generic;
using System;
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


    // Method untuk menampilkan task di komputer
    public void ShowTaskOnComputer()
    {
        // if (taskRejectCount >= taskRejectLimit)
        // {
        //     taskDescriptionText.text = "You have reached the task reject limit.";
        //     acceptButton.interactable = false;
        //     rejectButton.interactable = false;
        //     return;
        // }

        // Membuat task baru dengan budget, style, dan ruangan acak
        currentTask = new Task();
        taskDescriptionText.text = $"New Task: Design a {currentTask.room} with {currentTask.style} style. Budget: {currentTask.budget}";

        // Mengaktifkan tombol
        acceptButton.interactable = true;
        rejectButton.interactable = true;
    }

    // Accept task
    public void AcceptTask()
    {
        // taskDescriptionText.text = "Task accepted. Entering building mode...";
        // acceptButton.interactable = false;
        // rejectButton.interactable = false;

        // Pindahkan player ke building mode
        EnterBuildingMode();

        // Setel budget ke ShopManager
        shopManager.SetPlayerBudget(currentTask.budget);
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
        FindObjectOfType<ShopManager>().OpenShop();
    }
}
