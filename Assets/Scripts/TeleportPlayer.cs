using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TeleportPlayer : MonoBehaviour
{
    public Button teleportButton;    // Referensi ke tombol UI yang akan ditekan untuk teleport
    public Transform teleportTarget; // Referensi ke lokasi tujuan teleport (misal: GameObject kosong di lokasi yang diinginkan)
    public GameObject player;        // Referensi ke player yang akan dipindahkan
    public FadeScreen fadeScreen;    // Referensi ke FadeScreen untuk mengatur fade in dan fade out

    private void Start()
    {
        // Pastikan tombol terhubung ke method TeleportPlayerPosition saat ditekan
        if (teleportButton != null)
        {
            teleportButton.onClick.AddListener(TeleportWithFade);
        }
    }

    // Method untuk teleport dengan efek fade
    private void TeleportWithFade()
    {
        if (fadeScreen != null)
        {
            StartCoroutine(FadeAndTeleport());
        }
        else
        {
            // Jika fadeScreen tidak diatur, langsung teleport
            TeleportPlayerPosition();
        }
    }

    // Coroutine untuk melakukan fade out, teleport, lalu fade in
    private IEnumerator FadeAndTeleport()
    {
        // Fade Out
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        // Pindahkan player setelah fade out selesai
        TeleportPlayerPosition();

        // Fade In
        fadeScreen.FadeIn();
    }

    // Method untuk memindahkan posisi player ke posisi teleportTarget
    private void TeleportPlayerPosition()
    {
        if (player != null && teleportTarget != null)
        {
            player.transform.position = teleportTarget.position;
            Debug.Log("Player has been teleported to " + teleportTarget.position);
        }
        else
        {
            Debug.LogWarning("Player or Teleport Target is not set.");
        }
    }
}
