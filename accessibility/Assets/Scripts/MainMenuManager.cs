using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject ControlMenu;
    public GameObject AccessMenu;


    public void Quit()
    {
        Application.Quit();
    }

    public void ControlButton()
    {
        if (AccessMenu.activeSelf)
        {
            AccessMenu.SetActive(false);
            ControlMenu.SetActive(true);
        }
        else if (!ControlMenu.activeSelf)
        {
            ControlMenu.SetActive(true);
        }
        else if (ControlMenu.activeSelf)
        {
            ControlMenu.SetActive(false);
        }
    }

    public void AccessButton()
    {
        if (ControlMenu.activeSelf)
        {
            ControlMenu.SetActive(false);
            AccessMenu.SetActive(true);
        }
        else if (!AccessMenu.activeSelf)
        {
            AccessMenu.SetActive(true);
        }
        else if (AccessMenu.activeSelf)
        {
            AccessMenu.SetActive(false);
        }
    }
}
