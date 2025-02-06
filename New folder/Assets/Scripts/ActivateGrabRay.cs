using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ActivateGrabRay : MonoBehaviour
{
    public GameObject leftGrabRay;
    public GameObject rightGrabRay;

    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRDirectInteractor leftInteractor;
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRDirectInteractor rightInteractor;

    void Update()
    {
        leftGrabRay.SetActive(leftInteractor.interactablesSelected.Count == 0);
        rightGrabRay.SetActive(rightInteractor.interactablesSelected.Count == 0);
    }
    
}
