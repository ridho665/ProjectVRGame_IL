using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameManager gameManager;
    public Button continueButton;

    private void Start()
    {
        // Cek apakah data sudah ada di PlayerPrefs
        bool hasSaveData = PlayerPrefs.HasKey("IsNewGame");
        Debug.Log("Has Save Data: " + hasSaveData);
        
        // Mengaktifkan atau menonaktifkan tombol berdasarkan data yang ditemukan
        continueButton.interactable = hasSaveData;
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
        StartCoroutine(EnterGameplayModeWithFade());
    }

    public void Gameplay()
    {
        PlayerPrefs.SetInt("IsNewGame", 1);
        SceneManager.LoadScene("Gameplay");
        StartCoroutine(EnterLeaveGameplayModeWithFade());
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        // PlayerPrefs.SetInt("IsNewGame", 1);
        gameManager.StartNewGame();
        StartCoroutine(EnterGameplayModeWithFade());
    }

    public void ContinueGame()
    {
        if (PlayerPrefs.HasKey("IsNewGame"))
        {
            Debug.Log("Continuing Game...");
            gameManager.ContinueGame();
            StartCoroutine(EnterGameplayModeWithFade());
        }
        else
        {
            Debug.LogWarning("No saved data found, cannot continue.");
        }
    }

    public void BackMainMenu()
    {
        PlayerPrefs.DeleteAll();
        StartCoroutine(EnterMainMenuModeWithFade());
    }

    public void LeaveGameplay()
    {
    StartCoroutine(EnterLeaveGameplayModeWithFade());
    }

    public void PlayButtonSound()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(0);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }


    private IEnumerator EnterGameplayModeWithFade()
    {
        FadeScreen fadeScreen = FindObjectOfType<FadeScreen>(); // Mencari komponen FadeScreen di scene
        
        if (fadeScreen != null)
        {
            fadeScreen.FadeOut(); // Mulai fade out
            yield return new WaitForSeconds(fadeScreen.fadeDuration);
        }

        // Masuk ke building mode setelah fade selesai
        SceneManager.LoadScene("Gameplay");
    }

    private IEnumerator EnterMainMenuModeWithFade()
    {
        FadeScreen fadeScreen = FindObjectOfType<FadeScreen>(); // Mencari komponen FadeScreen di scene
        
        if (fadeScreen != null)
        {
            fadeScreen.FadeOut(); // Mulai fade out
            yield return new WaitForSeconds(fadeScreen.fadeDuration);
        }

        // Masuk ke building mode setelah fade selesai
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator EnterLeaveGameplayModeWithFade()
    {
        FadeScreen fadeScreen = FindObjectOfType<FadeScreen>(); // Mencari komponen FadeScreen di scene
        
        if (fadeScreen != null)
        {
            fadeScreen.FadeOut(); // Mulai fade out
            yield return new WaitForSeconds(fadeScreen.fadeDuration);
        }

        // Masuk ke building mode setelah fade selesai
        SceneManager.LoadScene("MainMenu");
    }
}
