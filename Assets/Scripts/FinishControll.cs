using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishControll : MonoBehaviour
{
    [SerializeField] private Button buttonYes;
    [SerializeField] private string targetSceneName;   // Nama scene tujuan
    [SerializeField] private FadeScreen fadeScreen;

    // Start is called before the first frame update
    void Start()
    {
        if (buttonYes != null)
        {
            buttonYes.onClick.AddListener(LoadSceneWithFade);
        } 
    }

    private void LoadSceneWithFade()
    {
        if (fadeScreen != null)
        {
            StartCoroutine(FadeAndLoadScene());
        }
        else
        {
            // Jika fadeScreen tidak diatur, langsung pindah scene
            LoadScene();
        }
    }

    private IEnumerator FadeAndLoadScene()
    {
        // Fade Out
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        // Pindah scene setelah fade out selesai
        LoadScene();
    }

    // Method untuk berpindah ke scene tujuan
    private void LoadScene()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
            Debug.Log("Scene is changing to " + targetSceneName);
        }
        else
        {
            Debug.LogWarning("Target Scene Name is not set.");
        }
    }

    public void PlaySoundClick()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(0);
        }
    }

    public void PlaySoundBuy()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(1);
        }
    }
}
