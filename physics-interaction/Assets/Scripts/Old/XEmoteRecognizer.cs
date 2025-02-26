using UnityEngine;
using System;

public class XEmoteRecognizer : MonoBehaviour
{
    public static event Action OnXDetected;

    [SerializeField] private QuadrantTracker quadrantTracker;

    private bool xEmoteTriggered = false;

    private void Update()
    {
        CheckXEmote();
    }

    private void CheckXEmote()
    {
        if (quadrantTracker.isLeftRT && quadrantTracker.isRightLT)
        {
            if (!xEmoteTriggered)
            {
                OnXDetected?.Invoke();
                xEmoteTriggered = true;
            }
        }
        else
        {
            xEmoteTriggered = false;
        }
    }
}
