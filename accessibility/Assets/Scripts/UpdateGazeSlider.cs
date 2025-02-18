using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UpdateGazeSlider : MonoBehaviour
{
    public Slider gazeTimeSlider;
    public TMP_Text timeText;

    public void UpdateGazeTime()
    {
        timeText.text = gazeTimeSlider.value.ToString("F2") + "s";
    }

    private void Start()
    {
        UpdateGazeTime();
    }
}
