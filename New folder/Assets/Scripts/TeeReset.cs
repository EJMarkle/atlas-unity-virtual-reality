using UnityEngine;

public class TeeReset : MonoBehaviour
{
    public GameObject[] balls;
    private Vector3[] originalPositions;
    
    void Start()
    {
        originalPositions = new Vector3[balls.Length];
        for (int i = 0; i < balls.Length; i++)
        {
            originalPositions[i] = balls[i].transform.position;
        }
    }

    public void ResetBalls()
    {
        for (int i = 0; i < balls.Length; i++)
        {
            GameObject ball = balls[i];
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            
            ball.transform.position = originalPositions[i];
            
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}