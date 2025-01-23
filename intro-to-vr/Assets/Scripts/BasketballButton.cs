using UnityEngine;

public class BasketballReset : MonoBehaviour
{
    public GameObject[] basketballs;
    private Vector3[] originalPositions;
    
    void Start()
    {
        originalPositions = new Vector3[basketballs.Length];
        for (int i = 0; i < basketballs.Length; i++)
        {
            originalPositions[i] = basketballs[i].transform.position;
        }
    }

    public void ResetBalls()
    {
        for (int i = 0; i < basketballs.Length; i++)
        {
            GameObject ball = basketballs[i];
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            
            ball.transform.position = originalPositions[i];
            
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}