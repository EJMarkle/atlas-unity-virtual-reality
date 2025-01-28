using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class MenuManager : MonoBehaviour
{
    public GameObject menu;
    public InputActionProperty showButton;
    public TMP_Dropdown sceneDropdown;

    void Awake()
    {
        DontDestroyOnLoad(menu);
    }
    
    void Update()
    {
        if(showButton.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);
        }
        
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void PlaySelectedScene()
    {
        int selectedIndex = sceneDropdown.value;
        string selectedSceneName = sceneDropdown.options[selectedIndex].text;
        SceneManager.LoadScene(selectedSceneName);
    }
}
