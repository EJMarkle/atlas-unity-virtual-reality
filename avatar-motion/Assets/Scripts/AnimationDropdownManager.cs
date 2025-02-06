using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEditor;

public class AnimationDropdownManager : MonoBehaviour
{
    public Dropdown animationDropdown;
    public Button playAnimationButton;
    public Animation dummyAnimation;

    private AnimationClip[] animationClips;

    void Start()
    {
        LoadAnimationClips();

        PopulateDropdown();

        playAnimationButton.onClick.AddListener(PlaySelectedAnimation);
    }

    void LoadAnimationClips()
    {
        string folderPath = "Assets/AnimationClips";
        animationClips = Directory.GetFiles(folderPath, "*.anim")
            .Select(path => AssetDatabase.LoadAssetAtPath<AnimationClip>(path))
            .Where(clip => clip != null)
            .ToArray();
    }

    void PopulateDropdown()
    {
        animationDropdown.ClearOptions();

        var clipNames = animationClips.Select(clip => clip.name).ToList();

        animationDropdown.AddOptions(clipNames);
    }

    void PlaySelectedAnimation()
    {
        int selectedIndex = animationDropdown.value;
        AnimationClip selectedClip = animationClips[selectedIndex];

        dummyAnimation.Play(selectedClip.name);
    }
}
