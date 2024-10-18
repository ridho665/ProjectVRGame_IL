using System.Collections;
using UnityEngine;

public class DestoryGameObject : MonoBehaviour
{
    public GameObject objectToDestroy;  // GameObject yang akan dihancurkan
    public GameObject vfxObject;        // Objek VFX yang akan diaktifkan setelah penghancuran
    public GameObject furnitureObject;  // Objek Furniture yang akan diaktifkan setelah VFX

    // public float delayAfterDestroy = 0.5f; // Penundaan setelah menghancurkan objectToDestroy
    public float delayBetweenVFXAndFurniture = 0.5f; // Penundaan antara aktivasi VFX dan Furniture

    // Fungsi untuk menghancurkan object awal dan mengaktifkan VFX dan Furniture
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            DestroyAndActivate();
        }    
    }

    public void DestroyAndActivate()
    {
        StartCoroutine(DestroyAndActivateSequence());
    }

    // Coroutine untuk menghancurkan objek awal, lalu mengaktifkan VFX dan Furniture
    private IEnumerator DestroyAndActivateSequence()
    {
        // Hancurkan objek awal terlebih dahulu
        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
            Debug.Log("Object awal telah dihancurkan.");
        }

        // Tunggu setelah menghancurkan objek
        // yield return new WaitForSeconds(delayAfterDestroy);

        // Aktifkan VFX setelah penghancuran
        if (vfxObject != null)
        {
            vfxObject.SetActive(true);
            // Debug.Log("VFX telah diaktifkan.");
        }

        // Tunggu sebelum mengaktifkan Furniture
        yield return new WaitForSeconds(delayBetweenVFXAndFurniture);

        // Aktifkan Furniture setelah VFX
        if (furnitureObject != null)
        {
            vfxObject.SetActive(false);
            furnitureObject.SetActive(true);
            // Debug.Log("Furniture telah diaktifkan.");
        }
    }

    // public void OnDestroy() 
    // {
    //     Destroy(gameObject);
    // }

}
