using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI budgetText; // Referensi ke UI Text untuk budget
    public TextMeshProUGUI styleText;  // Referensi ke UI Text untuk style
    public TextMeshProUGUI roomText;   // Referensi ke UI Text untuk room
    public TextMeshProUGUI timeText;

    private TaskManager taskManager; // Referensi ke TaskManager

    private void Start()
    {
        // Ambil referensi ke TaskManager dari GameObject yang sama
        taskManager = GetComponent<TaskManager>();

        // Mengambil referensi ke UI elements di scene, jika diperlukan
        budgetText = GameObject.Find("BudgetText")?.GetComponent<TextMeshProUGUI>();
        styleText = GameObject.Find("StyleText")?.GetComponent<TextMeshProUGUI>();
        roomText = GameObject.Find("RoomText")?.GetComponent<TextMeshProUGUI>();
        timeText = GameObject.Find("TimeText")?.GetComponent<TextMeshProUGUI>();

        UpdateTaskUI();
    }

    // Method untuk mengupdate UI dengan informasi task terbaru
    public void UpdateTaskUI()
    {
        if (taskManager.currentTask != null)
        {
            budgetText.text = "Budget 	: " + taskManager.currentTask.budget.ToString("C0"); // Format sebagai mata uang
            styleText.text = "Style 		: " + taskManager.currentTask.style;
            roomText.text = "Room 	: " + taskManager.currentTask.room;
            timeText.text = "Time            : " + FormatTime(taskManager.currentTask.time);
        }
    }

    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds); // Format menjadi MM:SS
    }
}
