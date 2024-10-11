using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        StartCoroutine(EnterGameplayModeWithFade());
    }

    public void ContinueGame()
    {
        StartCoroutine(EnterGameplayModeWithFade());
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
}
