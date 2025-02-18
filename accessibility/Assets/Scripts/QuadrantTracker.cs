using UnityEngine;


public class QuadrantTracker : MonoBehaviour
{
    public Collider leftTop, leftBottom, rightTop, rightBottom;

    public bool isLeftLT, isRightLT;
    public bool isLeftLB, isRightLB;
    public bool isLeftRT, isRightRT;
    public bool isLeftRB, isRightRB;

    private void OnTriggerEnter(Collider other)
    {
        if (!IsController(other, out bool isLeft, out bool isRight)) return;
        UpdateQuadrantState(other, isLeft, isRight);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!IsController(other, out bool isLeft, out bool isRight)) return;
        UpdateQuadrantState(other, isLeft, isRight);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsController(other, out bool isLeft, out bool isRight)) return;
        ClearQuadrantState(other, isLeft, isRight);
    }

    private void UpdateQuadrantState(Collider other, bool isLeft, bool isRight)
    {
        ResetQuadrants(isLeft, isRight);

        if (leftTop.bounds.Contains(other.transform.position))
        {
            isLeftLT |= isLeft;
            isRightLT |= isRight;
        }
        else if (leftBottom.bounds.Contains(other.transform.position))
        {
            isLeftLB |= isLeft;
            isRightLB |= isRight;
        }
        else if (rightTop.bounds.Contains(other.transform.position))
        {
            isLeftRT |= isLeft;
            isRightRT |= isRight;
        }
        else if (rightBottom.bounds.Contains(other.transform.position))
        {
            isLeftRB |= isLeft;
            isRightRB |= isRight;
        }

        //Debug.Log($"[QuadrantTracker] Updated State: LT({isLeftLT}, {isRightLT}), LB({isLeftLB}, {isRightLB}), RT({isLeftRT}, {isRightRT}), RB({isLeftRB}, {isRightRB})");
    }

    private void ClearQuadrantState(Collider other, bool isLeft, bool isRight)
    {
        if (leftTop.bounds.Contains(other.transform.position))
        {
            isLeftLT &= !isLeft;
            isRightLT &= !isRight;
        }
        else if (leftBottom.bounds.Contains(other.transform.position))
        {
            isLeftLB &= !isLeft;
            isRightLB &= !isRight;
        }
        else if (rightTop.bounds.Contains(other.transform.position))
        {
            isLeftRT &= !isLeft;
            isRightRT &= !isRight;
        }
        else if (rightBottom.bounds.Contains(other.transform.position))
        {
            isLeftRB &= !isLeft;
            isRightRB &= !isRight;
        }

        //Debug.Log($"[QuadrantTracker] Cleared State for {other.gameObject.name}");
    }

    private void ResetQuadrants(bool isLeft, bool isRight)
    {
        if (isLeft)
        {
            isLeftLT = false;
            isLeftLB = false;
            isLeftRT = false;
            isLeftRB = false;
        }
        if (isRight)
        {
            isRightLT = false;
            isRightLB = false;
            isRightRT = false;
            isRightRB = false;
        }
    }

    private bool IsController(Collider other, out bool isLeft, out bool isRight)
    {
        isLeft = other.CompareTag("LeftController");
        isRight = other.CompareTag("RightController");
        return isLeft || isRight;
    }
}
