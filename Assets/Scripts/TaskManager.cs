using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Tambahkan namespace untuk SceneManager

public class TaskManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject taskUI;
    public GameObject scoreTaskUI; // UI untuk menampilkan skor setelah task selesai
    // public TextMeshProUGUI taskDescriptionText; // UI untuk menampilkan deskripsi task
    public Button acceptButton;
    public Button rejectButton;
    public Button doneButton; // Tombol untuk menyelesaikan task pada scoreTaskUI
    private bool isTaskUIActive = true;
    private bool isScoreTaskUIActive = false; // Status scoreTaskUI

    [Header("References")]
    public ShopManager shopManager; // Referensi ke ShopManager untuk mengatur budget player
    public ScoreManager scoreManager;
    private TaskUIManager taskUIManager; // Referensi ke TaskUIManager

    [Header("Task Data")]
    public Task currentTask;

    private void Start()
    {
        // Muat status terakhir dari PlayerPrefs jika bukan pertama kali load
        isTaskUIActive = PlayerPrefs.GetInt("TaskUIActive", 1) == 1;
        isScoreTaskUIActive = PlayerPrefs.GetInt("ScoreTaskUIActive", 0) == 1;

        // Atur UI berdasarkan status yang tersimpan
        SetTaskUIActive(isTaskUIActive);
        SetScoreTaskUIActive(isScoreTaskUIActive);

        // Ambil referensi ke TaskUIManager dari GameObject yang sama
        taskUIManager = GetComponent<TaskUIManager>();

        // Hanya generate task jika TaskUI aktif
        if (isTaskUIActive)
        {
            GenerateInitialTask();
            GenerateNewTask();
        }

        // Tambahkan listener ke tombol-tombol
        acceptButton.onClick.AddListener(AcceptTask);
        rejectButton.onClick.AddListener(RejectTask);
        doneButton.onClick.AddListener(DoneTask); // Listener untuk tombol Done
    }

    private void GenerateInitialTask()
    {
        currentTask = new Task(); // Membuat task baru dengan nilai acak
        currentTask.Initialize();
        Debug.Log("Initial Task: " + currentTask.room + " with style: " + currentTask.style + " and budget: " + currentTask.budget);
    }

    public void GenerateNewTask()
    {
        currentTask = new Task(); // Membuat task baru
        currentTask.Initialize();  // Menginisialisasi task dengan nilai acak
        Debug.Log("New Task: " + currentTask.room + " with style: " + currentTask.style + " and budget: " + currentTask.budget);

        // Update UI setelah membuat task
        taskUIManager.UpdateTaskUI(); // Menggunakan referensi langsung ke TaskUIManager
    }

    // Accept task
    public void AcceptTask()
    {
        // Nonaktifkan TaskUI dan aktifkan ScoreTaskUI
        isTaskUIActive = false;
        // isScoreTaskUIActive = true;
        SetTaskUIActive(isTaskUIActive);
        // SetScoreTaskUIActive(isScoreTaskUIActive);

        // Simpan status UI ke PlayerPrefs
        PlayerPrefs.SetInt("TaskUIActive", isTaskUIActive ? 1 : 0);
        PlayerPrefs.SetInt("ScoreTaskUIActive", isScoreTaskUIActive ? 1 : 0);
        PlayerPrefs.Save();

        // Simpan data task ke GameManager
        GameManager.Instance.SaveGameData(currentTask.budget, currentTask.style, currentTask.room, currentTask.time, GameManager.Instance.playerMoney);

        // Setel budget ke ShopManager
        shopManager.SetPlayerBudget(currentTask.budget);

        // Setel waktu task ke ShopManager
        shopManager.SetTaskTime(currentTask.time);

        // Pindahkan player ke building mode (pindah scene sesuai dengan room)
        StartCoroutine(EnterBuildingModeWithFade(currentTask.room));
    }

    // Reject task
    public void RejectTask()
    {
        GenerateNewTask();

        // Mengaktifkan kembali tombol
        acceptButton.interactable = true;
        rejectButton.interactable = true;
    }

    // Fungsi untuk tombol Done di scoreTaskUI
    public void DoneTask()
    {
        if (scoreManager != null && shopManager != null)
        {
            int currentMoney = scoreManager.GetMoney();
            GameManager.Instance.AddMoney(currentMoney);
            shopManager.AddMoney(currentMoney);
        }

        scoreManager.ResetScoreAndMoney();
        // Nonaktifkan ScoreTaskUI dan aktifkan kembali TaskUI
        isScoreTaskUIActive = false;
        isTaskUIActive = true;

        // GameManager.Instance.OnTaskDone();
        SetScoreTaskUIActive(isScoreTaskUIActive);
        SetTaskUIActive(isTaskUIActive);
    
        

        // Simpan status UI ke PlayerPrefs
        PlayerPrefs.SetInt("TaskUIActive", isTaskUIActive ? 1 : 0);
        PlayerPrefs.SetInt("ScoreTaskUIActive", isScoreTaskUIActive ? 1 : 0);
        PlayerPrefs.Save();

        // Generate new task after done
        GenerateNewTask();
    }

    private void SetTaskUIActive(bool active)
    {
        taskUI.SetActive(active);
    }

    private void SetScoreTaskUIActive(bool active)
    {
        scoreTaskUI.SetActive(active);
    }

    private void OnDisable()
    {
        // Simpan status UI saat script di-disable
        PlayerPrefs.SetInt("TaskUIActive", isTaskUIActive ? 1 : 0);
        PlayerPrefs.SetInt("ScoreTaskUIActive", isScoreTaskUIActive ? 1 : 0);
        PlayerPrefs.Save();
    }

    private IEnumerator EnterBuildingModeWithFade(string roomName)
    {
        // Aktifkan fade screen
        FadeScreen fadeScreen = FindObjectOfType<FadeScreen>(); // Mencari komponen FadeScreen di scene
        if (fadeScreen != null)
        {
            fadeScreen.FadeOut(); // Mulai fade out
            yield return new WaitForSeconds(fadeScreen.fadeDuration);

            isScoreTaskUIActive = true;
            SetScoreTaskUIActive(isScoreTaskUIActive);    
        }

        // Masuk ke building mode setelah fade selesai
        EnterBuildingMode(roomName);
    }

    // Method untuk memulai building mode dan pindah ke scene sesuai room
    private void EnterBuildingMode(string roomName)
    {
        if (!string.IsNullOrEmpty(roomName))
        {
            Debug.Log($"Entering building mode for room: {roomName}");
            SceneManager.LoadScene(roomName);  // Pindah ke scene berdasarkan nama room
        }
        else
        {
            Debug.LogWarning("Room name is not set!");
        }
    }
}