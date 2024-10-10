using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private float taskDuration = 480f; // 8 minutes default

    public void SetTaskTime(float time)
    {
        taskDuration = time;
        UpdateTimerUI();
    }

    public float GetTaskDuration()
    {
        return taskDuration;
    }

    private void UpdateTimerUI()
    {
        // Logic to update the UI with the new time
    }
}
