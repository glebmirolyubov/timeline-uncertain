using UnityEngine;

public class RagdollManager : MonoBehaviour
{
    void Awake()
    {
        Component[] ragdollRigidbodyArray;

        ragdollRigidbodyArray = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in ragdollRigidbodyArray)
        {
            rb.detectCollisions = false;
        }
    }
}
