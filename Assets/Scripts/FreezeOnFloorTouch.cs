using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class FreezeOnFloorTouch : MonoBehaviour
{
    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;
    private Coroutine freezeCoroutine;

    private void Awake()
    {
        // Ambil komponen Rigidbody dan XRGrabInteractable dari objek ini
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (rb == null)
        {
            Debug.LogWarning("Rigidbody not found on the object!");
        }

        if (grabInteractable == null)
        {
            Debug.LogWarning("XRGrabInteractable not found on the object!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Periksa apakah objek menyentuh lantai dengan tag "Floor"
        if (collision.gameObject.CompareTag("Floor"))
        {
            if (freezeCoroutine == null)
            {
                // Mulai Coroutine untuk freeze setelah 1 detik
                freezeCoroutine = StartCoroutine(FreezeAfterDelay(1f));
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Jika objek tidak lagi menyentuh lantai, hapus freeze posisi dan rotasi
        if (collision.gameObject.CompareTag("Floor"))
        {
            if (freezeCoroutine != null)
            {
                StopCoroutine(freezeCoroutine);
                freezeCoroutine = null;
            }

            rb.constraints = RigidbodyConstraints.None;
            Debug.Log("Object no longer touching the floor and is unfrozen.");
        }
    }

    private void OnEnable()
    {
        // Berlangganan ke event XRGrabInteractable saat di-grab atau dilepas
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }
    }

    private void OnDisable()
    {
        // Hapus langganan dari event saat di-grab atau dilepas
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
            grabInteractable.selectExited.RemoveListener(OnRelease);
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Hapus freeze posisi dan rotasi saat objek di-grab
        if (freezeCoroutine != null)
        {
            StopCoroutine(freezeCoroutine);
            freezeCoroutine = null;
        }

        rb.constraints = RigidbodyConstraints.None;
        Debug.Log("Object grabbed, constraints removed.");
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // Jika dilepas, cek apakah masih menyentuh lantai dan mulai Coroutine untuk freeze
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Floor"))
            {
                if (freezeCoroutine == null)
                {
                    freezeCoroutine = StartCoroutine(FreezeAfterDelay(1f));
                }
                break;
            }
        }
    }

    private IEnumerator FreezeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        Debug.Log("Object is frozen after delay.");
        freezeCoroutine = null; // Reset coroutine
    }
}
