using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement; // Required for scene management

public class VideoManager : MonoBehaviour
{
    private VideoPlayer videoPlayer; // Reference to the VideoPlayer component
    private AudioSource audioSource; // Reference to the AudioSource component
    public Canvas uiCanvas;


    void Start()
    {
        // Get VideoPlayer and AudioSource components attached to the GameObject
        videoPlayer = GetComponent<VideoPlayer>();
        audioSource = GetComponent<AudioSource>();

        // Set VideoPlayer to output audio to AudioSource
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack(0, true); // Enable the first audio track
        videoPlayer.SetTargetAudioSource(0, audioSource);

        // Ensure Play on Awake is disabled
        videoPlayer.playOnAwake = false;

       
        videoPlayer.loopPointReached += OnVideoFinished;

        // Prepare the video (preloads it into memory)
        videoPlayer.Prepare();
    }

    // Called when the video finishes playing
    private void OnVideoFinished(VideoPlayer vp)
    {
        LoadMainMenu();
    }

    // Play the video and audio
    public void PlayVideo()
    {
        HideUI();
        videoPlayer.Play();
        audioSource.Play(); // Ensure audio starts playing
    }

    // Pause the video and audio
 


    // Load the MainMenu scene
    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    private void HideUI()
    {
        if (uiCanvas != null)
        {
            uiCanvas.enabled = false;
        }
    }
}
