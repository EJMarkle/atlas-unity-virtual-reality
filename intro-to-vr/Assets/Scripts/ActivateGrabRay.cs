using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class ActivateGrabRay : MonoBehaviour
{
    public GameObject leftGrabRay;
    public GameObject rightGrabRay;

    public XRDirectInteractor leftInteractor;
    public XRDirectInteractor rightInteractor;

    void Update()
    {
        leftGrabRay.SetActive(leftInteractor.interactablesSelected.Count == 0);
        rightGrabRay.SetActive(rightInteractor.interactablesSelected.Count == 0);
    }
    
}
