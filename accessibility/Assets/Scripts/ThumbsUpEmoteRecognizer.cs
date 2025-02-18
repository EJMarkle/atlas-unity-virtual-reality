using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class ThumbsUpEmoteRecognizer : MonoBehaviour
{
    public static event Action OnThumbsUpDetected;

    [SerializeField]
    private InputActionReference leftGripAction;
    [SerializeField]
    private InputActionReference leftTriggerAction;
    [SerializeField]
    private InputActionReference rightGripAction;
    [SerializeField]
    private InputActionReference rightTriggerAction;

    private bool wasThumbsUpLastFrame = false;

    private void OnEnable()
    {
        leftGripAction?.action?.Enable();
        leftTriggerAction?.action?.Enable();
        rightGripAction?.action?.Enable();
        rightTriggerAction?.action?.Enable();
    }

    private void OnDisable()
    {
        leftGripAction?.action?.Disable();
        leftTriggerAction?.action?.Disable();
        rightGripAction?.action?.Disable();
        rightTriggerAction?.action?.Disable();
    }

    private void Update()
    {
        bool isThumbsUpNow = CheckForThumbsUp();

        if (isThumbsUpNow && !wasThumbsUpLastFrame)
        {
            OnThumbsUpDetected?.Invoke();
        }

        wasThumbsUpLastFrame = isThumbsUpNow;
    }

    private bool CheckForThumbsUp()
    {
        bool leftThumbsUp = CheckControllerThumbsUp(leftGripAction, leftTriggerAction);
        bool rightThumbsUp = CheckControllerThumbsUp(rightGripAction, rightTriggerAction);

        return leftThumbsUp || rightThumbsUp;
    }

    private bool CheckControllerThumbsUp(InputActionReference gripAction, InputActionReference triggerAction)
    {
        if (gripAction?.action == null || triggerAction?.action == null) return false;

        float gripValue = gripAction.action.ReadValue<float>();
        float triggerValue = triggerAction.action.ReadValue<float>();

        return gripValue > 0.5f && triggerValue > 0.5f;
    }
}