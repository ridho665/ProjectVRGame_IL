using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameManager gameManager;

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        // PlayerPrefs.SetInt("IsNewGame", 1);
        gameManager.StartNewGame();
        StartCoroutine(EnterGameplayModeWithFade());
    }

    public void ContinueGame()
    {
        // PlayerPrefs.SetInt("IsNewGame", 0);
        gameManager.ContinueGame();
        StartCoroutine(EnterGameplayModeWithFade());
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
