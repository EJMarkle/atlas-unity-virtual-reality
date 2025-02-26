using System;
using UnityEngine;

public class FPRecognizer : MonoBehaviour
{
    public static event Action OnFacepalmDetected;

    private void OnTriggerEnter(Collider other)
    {
        if (IsController(other))
        {
            //Debug.Log("[FPRecognizer] Facepalm detected!");
            OnFacepalmDetected?.Invoke();
        }
    }

    private bool IsController(Collider other)
    {
        return other.CompareTag("LeftController") || other.CompareTag("RightController");
    }
}
