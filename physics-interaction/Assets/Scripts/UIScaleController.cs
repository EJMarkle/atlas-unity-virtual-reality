using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIScaleController : MonoBehaviour
{
    [Header("Slider References")]
    [SerializeField] private Slider fontSizeSlider;
    [SerializeField] private Slider uiScaleSlider;
    
    [Header("Font Size Settings")]
    [SerializeField] private float minSizeMultiplier = 0.5f; 
    [SerializeField] private float maxSizeMultiplier = 2.0f;  
    
    [Header("UI Scale Settings")]
    [SerializeField] private float minUIScale = 0.5f;
    [SerializeField] private float maxUIScale = 2f;
    
    [SerializeField] private Transform uiContainer;
    
    private Dictionary<TextMeshProUGUI, float> originalFontSizes = new Dictionary<TextMeshProUGUI, float>();
    private Vector3 originalUIScale;
    
    private void Start()
    {
        originalUIScale = uiContainer.localScale;
        
        fontSizeSlider.onValueChanged.AddListener(UpdateFontSize);
        uiScaleSlider.onValueChanged.AddListener(UpdateUIScale);
        
        fontSizeSlider.minValue = 0f;
        fontSizeSlider.maxValue = 1f;
        fontSizeSlider.value = 0.5f;
        
        uiScaleSlider.minValue = 0f;
        uiScaleSlider.maxValue = 1f;
        uiScaleSlider.value = 0.5f;
        
        RefreshTextComponents();
        UpdateFontSize(fontSizeSlider.value);
        UpdateUIScale(uiScaleSlider.value);
    }
    
    private void Update()
    {
        RefreshTextComponents();
    }
    
    private void RefreshTextComponents()
    {
        TextMeshProUGUI[] allTextComponents = FindObjectsByType<TextMeshProUGUI>(FindObjectsSortMode.None);
        
        bool foundNew = false;
        foreach (var text in allTextComponents)
        {
            if (text != null && !originalFontSizes.ContainsKey(text))
            {
                originalFontSizes.Add(text, text.fontSize);
                foundNew = true;
            }
        }
        
        if (foundNew)
        {
            UpdateFontSize(fontSizeSlider.value);
        }
    }
    
    private void UpdateFontSize(float sliderValue)
    {
        float multiplier;
        if (sliderValue <= 0.5f)
        {
            multiplier = Mathf.Lerp(minSizeMultiplier, 1.0f, sliderValue * 2);
        }
        else
        {
            multiplier = Mathf.Lerp(1.0f, maxSizeMultiplier, (sliderValue - 0.5f) * 2);
        }
        
        // Apply proportional scaling to all known text components
        foreach (var textEntry in originalFontSizes)
        {
            TextMeshProUGUI text = textEntry.Key;
            float originalSize = textEntry.Value;
            
            if (text != null)
            {
                text.fontSize = originalSize * multiplier;
            }
        }
    }
    
    private void UpdateUIScale(float sliderValue)
    {
        float scale = Mathf.Lerp(minUIScale, maxUIScale, sliderValue);
        uiContainer.localScale = originalUIScale * scale;
    }
}