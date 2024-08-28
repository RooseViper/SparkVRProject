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

    private void ReachedEnd() => expandButton.interactable = true;
}
