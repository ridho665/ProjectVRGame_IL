// using System.Collections;
// using UnityEngine;
// using UnityEngine.UI;

// public class TeleportPlayer : MonoBehaviour
// {
//     public GameObject TeleportUI;
//     public Button teleportButton;    // Referensi ke tombol UI yang akan ditekan untuk teleport
//     public Transform teleportTarget; // Referensi ke lokasi tujuan teleport (misal: GameObject kosong di lokasi yang diinginkan)
//     public GameObject player;        // Referensi ke player yang akan dipindahkan
//     public FadeScreen fadeScreen;    // Referensi ke FadeScreen untuk mengatur fade in dan fade out

//     private void Start()
//     {
//         // Pastikan tombol terhubung ke method TeleportPlayerPosition saat ditekan
//         if (teleportButton != null)
//         {
//             teleportButton.onClick.AddListener(TeleportWithFade);
//         }
//     }

//     // Method untuk teleport dengan efek fade
//     private void TeleportWithFade()
//     {
//         if (fadeScreen != null)
//         {
//             StartCoroutine(FadeAndTeleport());
//         }
//         else
//         {
//             // Jika fadeScreen tidak diatur, langsung teleport
//             TeleportPlayerPosition();
//         }
//     }

//     // Coroutine untuk melakukan fade out, teleport, lalu fade in
//     private IEnumerator FadeAndTeleport()
//     {
//         // Fade Out
//         fadeScreen.FadeOut();
//         yield return new WaitForSeconds(fadeScreen.fadeDuration);

//         // Pindahkan player setelah fade out selesai
//         TeleportPlayerPosition();

//         // Fade In
//         fadeScreen.FadeIn();
//         TeleportUI.SetActive(false);
//     }

//     // Method untuk memindahkan posisi player ke posisi teleportTarget
//     private void TeleportPlayerPosition()
//     {
//         if (player != null && teleportTarget != null)
//         {
//             player.transform.position = teleportTarget.position;
//             Debug.Log("Player has been teleported to " + teleportTarget.position);
//         }
//         else
//         {
//             Debug.LogWarning("Player or Teleport Target is not set.");
//         }
//     }
// }

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // Import SceneManager

public class TeleportPlayer : MonoBehaviour
{
    public GameObject TeleportUI;
    public Button teleportButton;    // Referensi ke tombol UI yang akan ditekan untuk berpindah scene
    public string targetSceneName;   // Nama scene tujuan
    public FadeScreen fadeScreen;    // Referensi ke FadeScreen untuk mengatur fade in dan fade out
    public ScoreManager scoreManager;    

    private void Start()
    {
        // Pastikan tombol terhubung ke method LoadSceneWithFade saat ditekan
        if (teleportButton != null)
        {
            teleportButton.onClick.AddListener(LoadSceneWithFade);
        }
    }

    // Method untuk berpindah scene dengan efek fade
    private void LoadSceneWithFade()
    {
        if (scoreManager != null)
        {
            scoreManager.SaveScoreAndMoney();
        }

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

    // Coroutine untuk melakukan fade out, pindah scene, lalu fade in di scene baru
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
}
