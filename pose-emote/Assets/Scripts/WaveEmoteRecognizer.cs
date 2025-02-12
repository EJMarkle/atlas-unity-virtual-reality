using UnityEngine;
using System;

public class WaveEmoteRecognizer : MonoBehaviour
{
    public static event Action OnWaveDetected;

    [SerializeField] private QuadrantTracker quadrantTracker;
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;

    [Header("Wave Detection Settings")]
    public float positionCheckInterval = 0.05f;
    public float minDistanceThreshold = 0.02f;
    public float minDirectionChangeTime = 0.2f;
    public int requiredDirectionChanges = 3;
    public float waveTimeout = 1.0f;

    private Vector3 lastLeftHandPos;
    private Vector3 lastRightHandPos;

    private int leftDirectionChanges = 0;
    private int rightDirectionChanges = 0;

    private bool isLeftMovingRight = false;
    private bool isRightMovingLeft = false;

    private float lastLeftChangeTime = 0f;
    private float lastRightChangeTime = 0f;
    private float lastPositionCheckTime = 0f;

    private void Start()
    {
        if (quadrantTracker == null)
        {
            Debug.LogError("[WaveEmoteRecognizer] QuadrantTracker reference is missing!");
        }
        if (leftHand == null || rightHand == null)
        {
            Debug.LogError("[WaveEmoteRecognizer] Hand references are missing!");
        }

        lastLeftHandPos = leftHand.position;
        lastRightHandPos = rightHand.position;
    }

    private void Update()
    {
        if (Time.time - lastPositionCheckTime >= positionCheckInterval)
        {
            TrackHandMovement();
            lastPositionCheckTime = Time.time;
        }

        CheckWaveGesture();
    }

    private void TrackHandMovement()
    {
        float currentTime = Time.time;

        if (quadrantTracker.isLeftLT)
        {
            float movementX = leftHand.position.x - lastLeftHandPos.x;

            if (Mathf.Abs(movementX) >= minDistanceThreshold)
            {
                bool movingRight = movementX > 0;

                if (movingRight != isLeftMovingRight && (currentTime - lastLeftChangeTime) > minDirectionChangeTime)
                {
                    leftDirectionChanges++;
                    isLeftMovingRight = movingRight;
                    lastLeftChangeTime = currentTime;
                    Debug.Log($"[WaveEmoteRecognizer] Left hand changed direction! Total changes: {leftDirectionChanges}");
                }
            }
        }
        else if (currentTime - lastLeftChangeTime > waveTimeout)
        {
            leftDirectionChanges = 0;
        }

        if (quadrantTracker.isRightRT)
        {
            float movementX = rightHand.position.x - lastRightHandPos.x;

            if (Mathf.Abs(movementX) >= minDistanceThreshold)
            {
                bool movingLeft = movementX < 0;

                if (movingLeft != isRightMovingLeft && (currentTime - lastRightChangeTime) > minDirectionChangeTime)
                {
                    rightDirectionChanges++;
                    isRightMovingLeft = movingLeft;
                    lastRightChangeTime = currentTime;
                    Debug.Log($"[WaveEmoteRecognizer] Right hand changed direction! Total changes: {rightDirectionChanges}");
                }
            }
        }
        else if (currentTime - lastRightChangeTime > waveTimeout)
        {
            rightDirectionChanges = 0;
        }

        lastLeftHandPos = leftHand.position;
        lastRightHandPos = rightHand.position;
    }

    private void CheckWaveGesture()
    {
        if (leftDirectionChanges >= requiredDirectionChanges || rightDirectionChanges >= requiredDirectionChanges)
        {
            OnWaveDetected?.Invoke();

            leftDirectionChanges = 0;
            rightDirectionChanges = 0;
        }
    }
}
