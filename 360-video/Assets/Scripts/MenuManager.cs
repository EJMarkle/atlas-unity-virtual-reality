using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


// Handles main menu buttons
public class MenuManager : MonoBehaviour
{
    public GameObject menu;
    public InputActionProperty showButton;
    public TMP_Dropdown sceneDropdown;

    
    void Update()
    {
        if(showButton.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);
        }
        
    }

    // Quit button functionality
    public void Quit()
    {
        Application.Quit();
    }

    // Load Video button functionality, switches to scene w appropriate video player
    public void PlaySelectedScene()
    {
        int selectedIndex = sceneDropdown.value;
        string selectedSceneName = sceneDropdown.options[selectedIndex].text;
        SceneManager.LoadScene(selectedSceneName);
    }
}
