using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    [SerializeField] private Transform panel;
    [SerializeField] private Button expandButton;
    [SerializeField] private AudioSource audioSource;
    [SerializeField]private List<VideoInfo> loadedVideos;
    [SerializeField] private Image[] displayImages;
    private VideoPlayer videoPlayer;
    private Vector3 expandPosition;
    private bool isExpanded;
    private void Start()
    {
        expandPosition = panel.localScale;
        panel.localScale = Vector3.zero;
        videoPlayer = GetComponent<VideoPlayer>();
        for (var i = 0; i < loadedVideos.Count; i++)
        {
            displayImages[i].sprite = loadedVideos[i].thumbnail;
            displayImages[i].GetComponentInChildren<TextMeshProUGUI>().text = loadedVideos[i].videoName;
        }
        expandButton.onClick.AddListener(ChangePanelSize);
        videoPlayer.Play();
        audioSource.mute = true;
    }

    public void PlayVideo(int index)
    {
        var vInfo = loadedVideos[index];
        videoPlayer.clip = vInfo.clip;
        videoPlayer.Play();
    }
    /// <summary>
    /// Expands or collapses the panel
    /// </summary>
    private void ChangePanelSize()
    {
        isExpanded =!isExpanded;
        if (isExpanded)
        {
            LeanTween.scale(panel.gameObject, expandPosition, 0.5f).setEaseInOutSine().setOnComplete(ReachedEnd);
            LeanTween.moveLocalX(expandButton.gameObject, 680f, 0.5f).setEaseInOutSine();
        }
        else
        {
            LeanTween.scale(panel.gameObject, Vector3.zero, 0.5f).setEaseInOutSine().setOnComplete(ReachedEnd);
            LeanTween.moveLocalX(expandButton.gameObject, -556f, 0.5f).setEaseInOutSine();
        }
        expandButton.interactable = false;
    }
    
    public void SkipForward()
    {
        if (videoPlayer == null || !videoPlayer.canSetTime) return;
        var newTime = videoPlayer.time + 10.0;
        videoPlayer.time = newTime < videoPlayer.length ? newTime : videoPlayer.length; // Set to the end if skipping past the video's length
    }
    
    public void SkipBackward()
    {
        if (videoPlayer == null || !videoPlayer.canSetTime) return;
        var newTime = videoPlayer.time - 10.0;
        if (newTime > 0) // Ensure we don't skip before the start of the video
        {
            videoPlayer.time = newTime;
        }
        else
        {
            videoPlayer.time = 0; // Set to the start if skipping past the beginning
        }
    }
    public void SetVolume(Slider slider) => audioSource.volume = slider.value;
    private void ReachedEnd() => expandButton.interactable = true;
}
