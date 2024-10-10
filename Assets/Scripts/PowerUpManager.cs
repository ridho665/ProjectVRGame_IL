using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpManager : MonoBehaviour
{
    [Header("PowerUp Settings")]
    // [SerializeField] private DynamicMoveProvider moveProvider;
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private TimerManager timerManager;

    [Header("UI Elements")]
    // [SerializeField] private Button sprintButton;
    [SerializeField] private Button budgetBoosterButton;
    [SerializeField] private Button timeExtensionButton;

    // [SerializeField] private TextMeshProUGUI sprintCooldownText;

    // private bool isSprintOnCooldown = false;
    // private float sprintCooldownDuration = 20f;
    // private float sprintBoostDuration = 5f;
    // private float speedBoostFactor = 1.2f; // 20% boost

    private void Start()
    {
        // Add listeners to buttons
        // sprintButton.onClick.AddListener(ActivateSprint);
        budgetBoosterButton.onClick.AddListener(ActivateBudgetBooster);
        timeExtensionButton.onClick.AddListener(ActivateTimeExtension);

        // UpdateSprintButtonUI();
    }

    // private void ActivateSprint()
    // {
    //     if (!isSprintOnCooldown && moveProvider != null)
    //     {
    //         StartCoroutine(SprintCoroutine());
    //     }
    // }

    // private IEnumerator SprintCoroutine()
    // {
    //     // Enable sprint boost
    //     moveProvider.speed *= speedBoostFactor;
    //     sprintButton.interactable = false; // Disable button during cooldown
    //     isSprintOnCooldown = true;

    //     // Start countdown for sprint duration (5 seconds)
    //     yield return new WaitForSeconds(sprintBoostDuration);

    //     // Reset speed after boost duration
    //     moveProvider.speed /= speedBoostFactor;

    //     // Start cooldown (20 seconds)
    //     float cooldownRemaining = sprintCooldownDuration;
    //     while (cooldownRemaining > 0)
    //     {
    //         cooldownRemaining -= Time.deltaTime;
    //         UpdateSprintCooldownUI(cooldownRemaining);
    //         yield return null;
    //     }

    //     // Reactivate sprint button
    //     isSprintOnCooldown = false;
    //     sprintButton.interactable = true;
    //     sprintCooldownText.text = ""; // Clear cooldown text
    // }

    // private void UpdateSprintCooldownUI(float cooldownRemaining)
    // {
    //     sprintCooldownText.text = $"Cooldown: {Mathf.Ceil(cooldownRemaining)}s";
    // }

    // private void UpdateSprintButtonUI()
    // {
    //     sprintButton.interactable = !isSprintOnCooldown;
    // }

    private void ActivateBudgetBooster()
    {
        if (shopManager != null)
        {
            shopManager.SetPlayerBudget(shopManager.GetPlayerBudget() + 100); // Add 100 to the budget
        }
    }

    private void ActivateTimeExtension()
    {
        if (timerManager != null && timerManager.GetTaskDuration() == 480f) // Check if task duration is exactly 8 minutes
        {
            timerManager.SetTaskTime(600f); // Set time to 10 minutes
        }
    }
}
