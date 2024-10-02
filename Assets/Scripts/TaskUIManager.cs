using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI budgetText; // Referensi ke UI Text untuk budget
    public TextMeshProUGUI styleText;  // Referensi ke UI Text untuk style
    public TextMeshProUGUI roomText;   // Referensi ke UI Text untuk room

    private TaskManager taskManager; // Referensi ke TaskManager

    private void Start()
    {
        taskManager = FindObjectOfType<TaskManager>();
        UpdateTaskUI();
    }

    // Method untuk mengupdate UI dengan informasi task terbaru
    public void UpdateTaskUI()
    {
        if (taskManager.currentTask != null)
        {
            budgetText.text = "Budget 	:" + taskManager.currentTask.budget.ToString("C0"); // Format sebagai mata uang
            styleText.text = "Style 		:" + taskManager.currentTask.style;
            roomText.text = "Room 	:" + taskManager.currentTask.room;
        }
    }
}
