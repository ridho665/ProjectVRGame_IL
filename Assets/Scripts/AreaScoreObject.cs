using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaScoreObject : MonoBehaviour
{
    [Header("Score Manager Reference")]
    public ScoreManager scoreManager; // Referensi ke ScoreManager

    private void OnTriggerEnter(Collider other)
    {
        // Memeriksa tag objek yang menyentuh area scoring
        switch (other.tag)
        {
            case "ChairClassic":
                scoreManager.AddScore(1); // Menambah skor 1 untuk chairClassic
                Debug.Log("chairClassic touched the score area. Score added: 1");
                break;

            case "ChairModern":
                scoreManager.AddScore(2); // Menambah skor 2 untuk chairModern
                Debug.Log("chairModern touched the score area. Score added: 2");
                break;

            case "TableClassic":
                scoreManager.AddScore(20); // Menambah skor 1 untuk TableClassic
                Debug.Log("TableClassic touched the score area. Score added: 1");
                break;

            case "TableModern":
                scoreManager.AddScore(2); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;

            default:
                Debug.Log("An object with an unrecognized tag touched the score area.");
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Memeriksa tag objek yang keluar dari area scoring
        switch (other.tag)
        {
            case "ChairClassic":
                scoreManager.AddScore(-1); // Mengurangi skor 1 untuk chairClassic
                Debug.Log("chairClassic left the score area. Score deducted: 1");
                break;

            case "ChairModern":
                scoreManager.AddScore(-2); // Mengurangi skor 2 untuk chairModern
                Debug.Log("chairModern left the score area. Score deducted: 2");
                break;

            case "TableClassic":
                scoreManager.AddScore(-20); // Mengurangi skor 1 untuk TableClassic
                Debug.Log("TableClassic left the score area. Score deducted: 1");
                break;

            case "TableModern":
                scoreManager.AddScore(-2); // Mengurangi skor 2 untuk TableModern
                Debug.Log("TableModern left the score area. Score deducted: 2");
                break;

            default:
                Debug.Log("An object with an unrecognized tag left the score area.");
                break;
        }
    }
}
