using UnityEngine;
using UnityEngine.Video;

public class VideoSequencePlayer : MonoBehaviour
{
    private VideoPlayer videoPlayer; // Assign the VideoPlayer component in the Inspector
    public VideoClip[] videoClips;  // Assign the video clips in the Inspector
    private int currentVideoIndex = 0;
    [SerializeField] private bool hasTimer;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    public void Play()
    {
        if (videoClips.Length > 0 && videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoFinished;
            PlayVideo(currentVideoIndex);
        }
        else
        {
            Debug.LogError("VideoPlayer or video clips not assigned!");
        }
    }

    private void PlayVideo(int index)
    {
        if (index < videoClips.Length)
        {
            videoPlayer.clip = videoClips[index];
            videoPlayer.Play();
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        currentVideoIndex++;
        if (currentVideoIndex < videoClips.Length)
        {
            if (hasTimer && currentVideoIndex == 1)
            {
                GetComponent<CountdownTimer>().StartCountDown();
            }
            PlayVideo(currentVideoIndex);
        }
        else
        {
         //   Debug.Log("All videos finished.");
        }
    }
}