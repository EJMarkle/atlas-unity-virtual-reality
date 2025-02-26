using UnityEngine;


[System.Serializable]
public class MapTransform
{
    public Transform vrTarget;
    public Transform IKTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void MapVRAvatar()
    {
        IKTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        IKTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}


public class AvatarController : MonoBehaviour
{
    [SerializeField] private MapTransform head;
    [SerializeField] private MapTransform leftHand;
    [SerializeField] private MapTransform rightHand;

    [SerializeField] private float positionSmoothness = 10f;
    [SerializeField] private float bodyTurnSmoothness = 5f;

    [SerializeField] private Transform IKHead;
    [SerializeField] private Vector3 headBodyOffset;

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, IKHead.position + headBodyOffset, Time.deltaTime * positionSmoothness);

        Vector3 projectedForward = Vector3.ProjectOnPlane(IKHead.forward, Vector3.up).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(projectedForward, Vector3.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * bodyTurnSmoothness);

        head.MapVRAvatar();
        leftHand.MapVRAvatar();
        rightHand.MapVRAvatar();
    }
}
