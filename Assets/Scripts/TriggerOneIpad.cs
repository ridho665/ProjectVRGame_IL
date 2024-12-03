using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOneIpad : MonoBehaviour
{
    public GameObject triggerOneIpad;

    public GameObject triggerControllerRight;
    public GameObject triggerControllerLeft;

    public GameObject gripControllerRight;
    public GameObject gripControllerLeft;

    public GameObject textOne;
    public GameObject textTwo;

    public GameObject raycastObject;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerOneIpad.SetActive(false);

            triggerControllerRight.SetActive(true);
            triggerControllerLeft.SetActive(true);
            gripControllerRight.SetActive(false);
            gripControllerLeft.SetActive(false);

            textOne.SetActive(false);
            textTwo.SetActive(true);

            raycastObject.SetActive(false);

            AudioManager.instance.PlaySFX(0);
        }
    }
}
