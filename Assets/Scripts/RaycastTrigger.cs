using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTrigger : MonoBehaviour
{   
    public GameObject iPad;
    public GameObject iPadTutorialUI;
    public GameObject cameraTutorialUI;

    public GameObject gripControllerRight;
    public GameObject gripControllerLeft;

    public GameObject controllerRight;
    public GameObject controllerLeft;


    private bool hasWall1BeenTouched = false;
    private bool hasWall2BeenTouched = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall1"))
        {
            hasWall1BeenTouched = true; // Tandai bahwa Wall1 sudah disentuh
            Debug.Log("Raycast object triggered collider with tag 'Wall1'.");
            AudioManager.instance.PlaySFX(0);
        }

    // Periksa apakah objek yang memicu collider memiliki tag "Wall2"
        if (other.CompareTag("Wall2"))
        {
            hasWall2BeenTouched = true; // Tandai bahwa Wall2 sudah disentuh
            Debug.Log("Raycast object triggered collider with tag 'Wall2'.");
            AudioManager.instance.PlaySFX(0);
        }

    // Aktifkan iPad jika kedua Wall1 dan Wall2 telah disentuh
        if (hasWall1BeenTouched && hasWall2BeenTouched)
        {
            gripControllerRight.SetActive(true);
            gripControllerLeft.SetActive(true);

            controllerRight.SetActive(false);
            controllerLeft.SetActive(false);

            cameraTutorialUI.SetActive(false);
            iPadTutorialUI.SetActive(true);
            iPad.SetActive(true); // Aktifkan GameObject iPad
            Debug.Log("Both Wall1 and Wall2 have been triggered. iPad activated.");

        }
    }
}
