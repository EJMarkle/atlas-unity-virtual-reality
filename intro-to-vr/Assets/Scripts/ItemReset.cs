using UnityEngine;

public class ItemsReset : MonoBehaviour
{
    public GameObject[] items; 
    private Vector3[] originalPositions; 
    private Quaternion[] originalRotations; 

    void Start()
    {
        originalPositions = new Vector3[items.Length];
        originalRotations = new Quaternion[items.Length];
        
        for (int i = 0; i < items.Length; i++)
        {
            originalPositions[i] = items[i].transform.position;
            originalRotations[i] = items[i].transform.rotation;
        }
    }

    public void ResetItems()
    {
        for (int i = 0; i < items.Length; i++)
        {
            GameObject item = items[i];
            Rigidbody rb = item.GetComponent<Rigidbody>();
            
            item.transform.position = originalPositions[i];
            item.transform.rotation = originalRotations[i];
            
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
