using UnityEngine;

public class IsFeatherMoving : MonoBehaviour
{
    public bool isMoving { get; private set; }

    private Rigidbody rb;
    private Animator animator;
    public float raycastDistance = 0.1f;

    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        isMoving = !IsMoving();
        (animator ??= GetComponent<Animator>())?.SetBool("isMoving", isMoving);
    }
    

    private bool IsMoving()
    {
        return Physics.Raycast(transform.position, Vector3.down, raycastDistance, groundLayer);
    }
}
