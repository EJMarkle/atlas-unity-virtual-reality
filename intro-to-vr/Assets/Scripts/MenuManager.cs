using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public MeshRenderer tunnelingVignette;

    public void Quit()
    {

        Application.Quit();
    }

    public void VignetteOn()
    {
        tunnelingVignette.enabled = true;
    }

    public void VignetteOff()
    {
        tunnelingVignette.enabled = false;
    }
}
