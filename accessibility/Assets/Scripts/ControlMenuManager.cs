using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;


public class ControlMenuManager : MonoBehaviour
{
    public ControllerInputActionManager leftController;
    public ControllerInputActionManager rightController;

    public void MoveDropdown(int index)
    {
        if (index == 0)
        {
            leftController.smoothMotionEnabled = true;
        }
        else if (index == 1)
        {
            leftController.smoothMotionEnabled = false;
        }
    }

    public void TurnDropdown(int index)
    {
        if (index == 0)
        {
            rightController.smoothTurnEnabled = true;
        }
        else if (index == 1)
        {
            rightController.smoothTurnEnabled = false;
        }
    }
}
