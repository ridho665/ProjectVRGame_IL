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
            case "KursiModern":
                scoreManager.AddScore(3); // Menambah skor 1 untuk chairClassic
                Debug.Log("chairClassic touched the score area. Score added: 1");
                break;

            case "KursiKlasik":
                scoreManager.AddScore(5); // Menambah skor 2 untuk chairModern
                Debug.Log("chairModern touched the score area. Score added: 2");
                break;

            case "MejaModern":
                scoreManager.AddScore(5); // Menambah skor 1 untuk TableClassic
                Debug.Log("TableClassic touched the score area. Score added: 1");
                break;

            case "MejaKlasik":
                scoreManager.AddScore(10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "MejaRiasModern":
                scoreManager.AddScore(5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "MejaRiasKlasik":
                scoreManager.AddScore(10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "RakBukuModern":
                scoreManager.AddScore(10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "RakBukuKlasik":
                scoreManager.AddScore(5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "SofaModern":
                scoreManager.AddScore(10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "SofaKlasik":
                scoreManager.AddScore(10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "TempatTidurModern":
                scoreManager.AddScore(10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "TempatTidurKlasik":
                scoreManager.AddScore(5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "BingkaiFotoModern":
                scoreManager.AddScore(3); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "BingkaiFotoKlasik":
                scoreManager.AddScore(7); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "BonsaiModern":
                scoreManager.AddScore(15); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "BonsaiKlasik":
                scoreManager.AddScore(15); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "CerminModern":
                scoreManager.AddScore(7); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "CerminKlasik":
                scoreManager.AddScore(3); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "JamDindingModern":
                scoreManager.AddScore(5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "JamDindingKlasik":
                scoreManager.AddScore(10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "KarpetModern":
                scoreManager.AddScore(10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "KarpetKlasik":
                scoreManager.AddScore(10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "LampuHiasModern":
                scoreManager.AddScore(5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "LampuHiasKlasik":
                scoreManager.AddScore(10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "PatungHiasModern":
                scoreManager.AddScore(10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "PatungHiasKlasik":
                scoreManager.AddScore(5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "TiraiModern":
                scoreManager.AddScore(10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "TiraiKlasik":
                scoreManager.AddScore(5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "VasBungaModern":
                scoreManager.AddScore(5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "VasBungaKlasik":
                scoreManager.AddScore(3); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "KomporModern":
                scoreManager.AddScore(5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "KomporKlasik":
                scoreManager.AddScore(10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "KomputerModern":
                scoreManager.AddScore(15); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "KomputerKlasik":
                scoreManager.AddScore(5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "KulkasModern":
                scoreManager.AddScore(10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "KulkasKlasik":
                scoreManager.AddScore(5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "MesinCuciModern":
                scoreManager.AddScore(5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "MesinCuciKlasik":
                scoreManager.AddScore(5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "MicrowaveModern":
                scoreManager.AddScore(5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "MicrowaveKlasik":
                scoreManager.AddScore(10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "RakTvModern":
                scoreManager.AddScore(5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "RakTvKlasik":
                scoreManager.AddScore(15); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "TvModern":
                scoreManager.AddScore(10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "TvKlasik":
                scoreManager.AddScore(5); // Menambah skor 2 untuk TableModern
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
            case "KursiModern":
                scoreManager.AddScore(-3); // Menambah skor 1 untuk chairClassic
                Debug.Log("chairClassic touched the score area. Score added: 1");
                break;

            case "KursiKlasik":
                scoreManager.AddScore(-5); // Menambah skor 2 untuk chairModern
                Debug.Log("chairModern touched the score area. Score added: 2");
                break;

            case "MejaModern":
                scoreManager.AddScore(-5); // Menambah skor 1 untuk TableClassic
                Debug.Log("TableClassic touched the score area. Score added: 1");
                break;

            case "MejaKlasik":
                scoreManager.AddScore(-10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "MejaRiasModern":
                scoreManager.AddScore(-5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "MejaRiasKlasik":
                scoreManager.AddScore(-10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "RakBukuModern":
                scoreManager.AddScore(-10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "RakBukuKlasik":
                scoreManager.AddScore(-5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "SofaModern":
                scoreManager.AddScore(-10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "SofaKlasik":
                scoreManager.AddScore(-10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "TempatTidurModern":
                scoreManager.AddScore(-10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "TempatTidurKlasik":
                scoreManager.AddScore(-5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "BingkaiFotoModern":
                scoreManager.AddScore(-3); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "BingkaiFotoKlasik":
                scoreManager.AddScore(-7); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "BonsaiModern":
                scoreManager.AddScore(-15); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "BonsaiKlasik":
                scoreManager.AddScore(-15); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "CerminModern":
                scoreManager.AddScore(-7); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "CerminKlasik":
                scoreManager.AddScore(-3); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "JamDindingModern":
                scoreManager.AddScore(-5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "JamDindingKlasik":
                scoreManager.AddScore(-10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "KarpetModern":
                scoreManager.AddScore(-10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "KarpetKlasik":
                scoreManager.AddScore(-10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "LampuHiasModern":
                scoreManager.AddScore(-5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "LampuHiasKlasik":
                scoreManager.AddScore(-10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "PatungHiasModern":
                scoreManager.AddScore(-10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "PatungHiasKlasik":
                scoreManager.AddScore(-5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "TiraiModern":
                scoreManager.AddScore(-10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "TiraiKlasik":
                scoreManager.AddScore(-5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "VasBungaModern":
                scoreManager.AddScore(-5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "VasBungaKlasik":
                scoreManager.AddScore(-3); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "KomporModern":
                scoreManager.AddScore(-5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "KomporKlasik":
                scoreManager.AddScore(-10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "KomputerModern":
                scoreManager.AddScore(-15); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "KomputerKlasik":
                scoreManager.AddScore(-5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "KulkasModern":
                scoreManager.AddScore(-10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "KulkasKlasik":
                scoreManager.AddScore(-5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "MesinCuciModern":
                scoreManager.AddScore(-5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "MesinCuciKlasik":
                scoreManager.AddScore(-5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "MicrowaveModern":
                scoreManager.AddScore(-5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "MicrowaveKlasik":
                scoreManager.AddScore(-10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "RakTvModern":
                scoreManager.AddScore(-5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "RakTvKlasik":
                scoreManager.AddScore(-15); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "TvModern":
                scoreManager.AddScore(-10); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;
            case "TvKlasik":
                scoreManager.AddScore(-5); // Menambah skor 2 untuk TableModern
                Debug.Log("TableModern touched the score area. Score added: 2");
                break;

            default:
                Debug.Log("An object with an unrecognized tag left the score area.");
                break;
        }
    }
}
