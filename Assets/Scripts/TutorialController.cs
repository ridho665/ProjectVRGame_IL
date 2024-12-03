using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TutorialController : MonoBehaviour
{
    public GameObject triggerTutorialUI;
    public GameObject cameraUI; // Referensi ke UI Camera\

    public GameObject controllerRight;
    public GameObject controllerLeft;

    public GameObject triggerControllerRight;
    public GameObject triggerControllerLeft;
    public GameObject gripControllerRight;
    public GameObject gripControllerLeft;

    public GameObject raycastObject;
    public GameObject cameraTriggerObject;
    // public GameObject iPad;

    public XRController leftController; // Referensi ke Left XR Controller
    public XRController rightController; // Referensi ke Right XR Controller

    public InputHelpers.Button triggerButton = InputHelpers.Button.Trigger; // Tombol untuk trigger
    public InputHelpers.Button grabButton = InputHelpers.Button.Grip; // Tombol untuk grab

    private bool hasTriggerBeenPressed = false; // Status apakah trigger sudah ditekan
    private bool hasGrabBeenPressed = false; // Status apakah grab sudah ditekan
    private bool isTutorialShown = false; // Status apakah UI sudah ditampilkan

    void Update()
    {
        if (cameraUI != null && !isTutorialShown)
        {
            // Periksa apakah trigger pada controller kiri atau kanan ditekan
            if (!hasTriggerBeenPressed)
            {
                if ((leftController != null && IsButtonPressed(leftController, triggerButton)) ||
                    (rightController != null && IsButtonPressed(rightController, triggerButton)))
                {
                    triggerControllerRight.SetActive(false);
                    triggerControllerLeft.SetActive(false);
                    gripControllerRight.SetActive(true);
                    gripControllerLeft.SetActive(true);
                    
                    hasTriggerBeenPressed = true;
                    Debug.Log("Trigger button pressed.");

                    AudioManager.instance.PlaySFX(0);
                }
            }

            // Periksa apakah grab pada controller kiri atau kanan ditekan setelah trigger
            if (hasTriggerBeenPressed && !hasGrabBeenPressed)
            {
                if ((leftController != null && IsButtonPressed(leftController, grabButton)) ||
                    (rightController != null && IsButtonPressed(rightController, grabButton)))
                {

                    hasGrabBeenPressed = true;
                    Debug.Log("Grab button pressed.");
                }
            }

            // Aktifkan UI jika kedua kondisi terpenuhi
            if (hasTriggerBeenPressed && hasGrabBeenPressed)
            {

                controllerRight.SetActive(true);
                controllerLeft.SetActive(true);
                gripControllerRight.SetActive(false);
                gripControllerLeft.SetActive(false);
                
                cameraTriggerObject.SetActive(true);
                raycastObject.SetActive(true);

                triggerTutorialUI.SetActive(false); // Nonaktifkan tutorial trigger
                cameraUI.SetActive(true); // Aktifkan UI kamera
                isTutorialShown = true; // Cegah UI ditampilkan ulang
                Debug.Log("Both trigger and grab buttons pressed, camera UI activated.");
                
                AudioManager.instance.PlaySFX(0);
            }
        }
    }

    // private bool hasWall1BeenTouched = false;
    // private bool hasWall2BeenTouched = false;

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Wall1") && raycastObject != null)
    //     {
    //         hasWall1BeenTouched = true; // Tandai bahwa Wall1 sudah disentuh
    //         Debug.Log("Raycast object triggered collider with tag 'Wall1'.");
    //     }

    // // Periksa apakah objek yang memicu collider memiliki tag "Wall2"
    //     if (other.CompareTag("Wall2") && raycastObject != null)
    //     {
    //         hasWall2BeenTouched = true; // Tandai bahwa Wall2 sudah disentuh
    //         Debug.Log("Raycast object triggered collider with tag 'Wall2'.");
    //     }

    // // Aktifkan iPad jika kedua Wall1 dan Wall2 telah disentuh
    //     if (hasWall1BeenTouched && hasWall2BeenTouched)
    //     {
    //         iPad.SetActive(true); // Aktifkan GameObject iPad
    //         Debug.Log("Both Wall1 and Wall2 have been triggered. iPad activated.");
    //     }
    // }

    // Metode untuk memeriksa apakah tombol tertentu ditekan
    private bool IsButtonPressed(XRController controller, InputHelpers.Button button)
    {
        InputHelpers.IsPressed(controller.inputDevice, button, out bool isPressed);
        return isPressed;
    }
}
