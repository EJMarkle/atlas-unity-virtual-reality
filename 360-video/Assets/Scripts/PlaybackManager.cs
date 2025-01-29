using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

// Handles playback buttons
public class PlaybackManager : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private bool isLooping = false;
    public Button playPauseButton;
    public Sprite playSprite;
    public Sprite pauseSprite; 
    public Button speedButton;
    public Sprite speed1xSprite;
    public Sprite speed1_5xSprite;
    public Sprite speed2xSprite;
    public Sprite speed0_5xSprite;
    private PlaybackSpeed currentSpeed = PlaybackSpeed.Speed1x;


    // Enum for playback speeds
    public enum PlaybackSpeed
    {
        Speed1x,  
        Speed1_5x,  
        Speed2x,   
        Speed0_5x   
    }

    void Start()
    {
        FindVideoPlayer();
        UpdatePlayPauseButton();
        UpdateSpeedButton();
    }

    // Get whatever video player is in the scene
    void FindVideoPlayer()
    {
        videoPlayer = FindObjectOfType<VideoPlayer>();
    }

    // Play button functionality
    public void Play()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Play();
            UpdatePlayPauseButton();
        }
    }

    // Pause button functionality
    public void Pause()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Pause();
            UpdatePlayPauseButton();
        }
    }

    // Play/Pause from one button
    public void PlayPause()
    {
        if (videoPlayer != null)
        {
            if (IsVideoPlaying())
            {
                Pause();
            }
            else
            {
                Play();
            }
        }
    }

    // Skip ahead button functionality
    public void SkipForward15()
    {
        if (videoPlayer != null)
        {
            videoPlayer.time += 15f;
        }
    }

    // Skip back button functionality
    public void SkipBack15()
    {
        if (videoPlayer != null)
        {
            videoPlayer.time -= 15f;
            if (videoPlayer.time < 0) videoPlayer.time = 0;
        }
    }

    // Loop toggle functionality
    public void Loop()
    {
        if (videoPlayer != null)
        {
            isLooping = !isLooping;
            videoPlayer.isLooping = isLooping;
        }
    }

    public bool IsVideoPlaying()
    {
        return videoPlayer != null && videoPlayer.isPlaying;
    }

    // Display apprpriate sprite on IsVideoPlaying()
    void UpdatePlayPauseButton()
    {
        if (playPauseButton != null && playSprite != null && pauseSprite != null)
        {
            playPauseButton.image.sprite = IsVideoPlaying() ? pauseSprite : playSprite;
        }
    }

    // Playback speed button functionality using enum
    public void CycleSpeed()
    {
        currentSpeed = (PlaybackSpeed)(((int)currentSpeed + 1) % 4);
        UpdateSpeedButton();

        switch (currentSpeed)
        {
            case PlaybackSpeed.Speed1x:
                videoPlayer.playbackSpeed = 1f;
                break;
            case PlaybackSpeed.Speed1_5x:
                videoPlayer.playbackSpeed = 1.5f;
                break;
            case PlaybackSpeed.Speed2x:
                videoPlayer.playbackSpeed = 2f;
                break;
            case PlaybackSpeed.Speed0_5x:
                videoPlayer.playbackSpeed = 0.5f;
                break;
        }
    }

    // Update button sprite based on play speed
    private void UpdateSpeedButton()
    {
        Image buttonImage = speedButton.GetComponent<Image>();

        switch (currentSpeed)
        {
            case PlaybackSpeed.Speed1x:
                buttonImage.sprite = speed1xSprite;
                break;
            case PlaybackSpeed.Speed1_5x:
                buttonImage.sprite = speed1_5xSprite;
                break;
            case PlaybackSpeed.Speed2x:
                buttonImage.sprite = speed2xSprite;
                break;
            case PlaybackSpeed.Speed0_5x:
                buttonImage.sprite = speed0_5xSprite;
                break;
        }
    }

    void Update()
    {
        UpdatePlayPauseButton();
        if (videoPlayer == null)
        {
            FindVideoPlayer();
        }
    }
}
