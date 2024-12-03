using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoneAreaObject : MonoBehaviour
{
    public GameObject winMenuUI;
    public GameObject doneAreaObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KursiKlasik"))
        {
            winMenuUI.SetActive(true);
            doneAreaObject.SetActive(false);

            AudioManager.instance.PlaySFX(0);
        }
    }
}
