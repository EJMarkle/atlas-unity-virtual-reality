using UnityEngine;
using System.Collections.Generic;

public class ResetManager : MonoBehaviour
{
    public List<GameObject> objectsToReset = new List<GameObject>();
    
    private Dictionary<GameObject, (Vector3, Quaternion)> originalTransforms = new Dictionary<GameObject, (Vector3, Quaternion)>();

    void Start()
    {
        StoreOriginalTransforms();
    }

    private void StoreOriginalTransforms()
    {
        originalTransforms.Clear();
        
        foreach (GameObject obj in objectsToReset)
        {
            if (obj != null)
            {
                originalTransforms[obj] = (obj.transform.position, obj.transform.rotation);
            }
        }
    }

    public void ResetItems()
    {
        foreach (var obj in originalTransforms)
        {
            if (obj.Key != null)
            {
                obj.Key.transform.position = obj.Value.Item1;
                obj.Key.transform.rotation = obj.Value.Item2;
            }

            Rigidbody rb = obj.Key.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
