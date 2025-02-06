using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class AnimationRecorder : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Transform playerRig;

    [Header("Settings")]
    [SerializeField] private float recordingFrameRate = 30f;

    private Transform armature;
    private bool isRecording = false;
    private float recordingTimer = 0f;
    private List<AnimationClip> recordedAnimations = new List<AnimationClip>();
    private List<Transform> trackedBones = new List<Transform>();
    private List<FrameData> currentRecording = new List<FrameData>();

    private void Start()
    {
        armature = playerRig.Find("Armature");
        if (armature == null)
        {
            Debug.LogError("Armature not found.");
            return;
        }

        CollectBonesToTrack(armature);
    }

    private void CollectBonesToTrack(Transform root)
    {
        foreach (Transform child in root)
        {
            trackedBones.Add(child);
            CollectBonesToTrack(child);
        }
    }

    public void StartRecording()
    {
        if (!isRecording)
        {
            isRecording = true;
            currentRecording.Clear();
            recordingTimer = 0f;
        }
    }

    public void StopRecording()
    {
        if (isRecording)
        {
            isRecording = false;
            SaveRecording();
        }
    }

    private void Update()
    {
        if (isRecording)
        {
            recordingTimer += Time.deltaTime;

            if (recordingTimer >= 1f / recordingFrameRate)
            {
                RecordFrame();
                recordingTimer = 0f;
            }
        }
    }

    private void RecordFrame()
    {
        FrameData frame = new FrameData
        {
            timestamp = Time.time,
            boneData = new List<BoneData>()
        };

        foreach (Transform bone in trackedBones)
        {
            frame.boneData.Add(new BoneData
            {
                boneName = bone.name,
                localPosition = bone.localPosition,
                localRotation = bone.localRotation
            });
        }

        currentRecording.Add(frame);
    }

    private void SaveRecording()
    {
        AnimationClip clip = new AnimationClip
        {
            name = $"Animation{recordedAnimations.Count + 1}",
            legacy = false,
            wrapMode = WrapMode.Loop
        };

        Debug.Log($"Created clip: {clip.name}");

        foreach (var bone in trackedBones)
        {
            CreateCurvesForBone(clip, bone);
        }

        string directoryPath = "Assets/AnimationClips";
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        string path = $"{directoryPath}/{clip.name}.anim";
        AssetDatabase.CreateAsset(clip, path);
        AssetDatabase.SaveAssets();

        recordedAnimations.Add(clip);

        Debug.Log($"Recorded Animations: {string.Join(", ", recordedAnimations.Select(a => a.name))}");
    }

    private void CreateCurvesForBone(AnimationClip clip, Transform bone)
    {
        string path = GetFullPath(bone);

        AnimationCurve curveX = new AnimationCurve();
        AnimationCurve curveY = new AnimationCurve();
        AnimationCurve curveZ = new AnimationCurve();
        
        AnimationCurve curveRotX = new AnimationCurve();
        AnimationCurve curveRotY = new AnimationCurve();
        AnimationCurve curveRotZ = new AnimationCurve();
        AnimationCurve curveRotW = new AnimationCurve();

        foreach (var frame in currentRecording)
        {
            var boneData = frame.boneData.Find(b => b.boneName == bone.name);
            if (boneData != null)
            {
                float time = frame.timestamp;

                curveX.AddKey(time, boneData.localPosition.x);
                curveY.AddKey(time, boneData.localPosition.y);
                curveZ.AddKey(time, boneData.localPosition.z);

                curveRotX.AddKey(time, boneData.localRotation.x);
                curveRotY.AddKey(time, boneData.localRotation.y);
                curveRotZ.AddKey(time, boneData.localRotation.z);
                curveRotW.AddKey(time, boneData.localRotation.w);
            }
        }

        clip.SetCurve(path, typeof(Transform), "localPosition.x", curveX);
        clip.SetCurve(path, typeof(Transform), "localPosition.y", curveY);
        clip.SetCurve(path, typeof(Transform), "localPosition.z", curveZ);

        clip.SetCurve(path, typeof(Transform), "localRotation.x", curveRotX);
        clip.SetCurve(path, typeof(Transform), "localRotation.y", curveRotY);
        clip.SetCurve(path, typeof(Transform), "localRotation.z", curveRotZ);
        clip.SetCurve(path, typeof(Transform), "localRotation.w", curveRotW);
    }

    private string GetFullPath(Transform current)
    {
        string path = current.name;
        Transform parent = current.parent;

        while (parent != null && parent != playerRig)
        {
            path = parent.name + "/" + path;
            parent = parent.parent;
        }
        return path;
    }

    private void PopulateDropdownWithAnimations()
    {
        string[] animationPaths = Directory.GetFiles("Assets/AnimationClips", "*.anim", SearchOption.TopDirectoryOnly);
        
        List<string> animationNames = animationPaths.Select(path => Path.GetFileNameWithoutExtension(path)).ToList();
    }

    [System.Serializable]
    public class FrameData
    {
        public float timestamp;
        public List<BoneData> boneData;
    }

    [System.Serializable]
    public class BoneData
    {
        public string boneName;
        public Vector3 localPosition;
        public Quaternion localRotation;
    }
}
