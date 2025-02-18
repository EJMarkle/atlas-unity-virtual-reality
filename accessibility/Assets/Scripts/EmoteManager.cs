using UnityEngine;
using System.Collections;


public class EmoteManager : MonoBehaviour
{
    [SerializeField] private GameObject bubble;
    [SerializeField] private GameObject thumbsUp;
    [SerializeField] private GameObject x;
    [SerializeField] private GameObject facepalm;
    [SerializeField] private GameObject wave;

    private bool isDisplayingEmote = false;

    private void OnEnable()
    {
        WaveEmoteRecognizer.OnWaveDetected += HandleWave;
        XEmoteRecognizer.OnXDetected += HandleX;
        FPRecognizer.OnFacepalmDetected += HandleFacepalm;
        ThumbsUpEmoteRecognizer.OnThumbsUpDetected += HandleThumbsUp;
    }

    private void OnDisable()
    {
        WaveEmoteRecognizer.OnWaveDetected -= HandleWave;
        XEmoteRecognizer.OnXDetected -= HandleX;
        FPRecognizer.OnFacepalmDetected -= HandleFacepalm;
        ThumbsUpEmoteRecognizer.OnThumbsUpDetected -= HandleThumbsUp;
    }

    private void HandleWave() => ShowEmote(wave, "[EmoteManager] Wave detected!");
    private void HandleX() => ShowEmote(x, "[EmoteManager] X pose detected!");
    private void HandleFacepalm() => ShowEmote(facepalm, "[EmoteManager] Facepalm detected!");
    private void HandleThumbsUp() => ShowEmote(thumbsUp, "[EmoteManager] Thumbs up detected!");

    private void ShowEmote(GameObject emoteObject, string logMessage)
    {
        if (isDisplayingEmote) return;

        //Debug.Log(logMessage);
        StartCoroutine(DisplayEmote(emoteObject));
    }

    private IEnumerator DisplayEmote(GameObject emoteObject)
    {
        isDisplayingEmote = true;

        bubble.SetActive(true);
        emoteObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        emoteObject.SetActive(false);
        bubble.SetActive(false);

        isDisplayingEmote = false;
    }
}
