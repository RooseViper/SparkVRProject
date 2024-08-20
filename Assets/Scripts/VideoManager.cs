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
    [SerializeField]private List<VideoInfo> loadedVideos;
    [SerializeField] private Image[] displayImages;
    private VideoPlayer videoPlayer;
    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        for (var i = 0; i < loadedVideos.Count; i++)
        {
            displayImages[i].sprite = loadedVideos[i].thumbnail;
            displayImages[i].GetComponentInChildren<TextMeshProUGUI>().text = loadedVideos[i].videoName;
        }
        videoPlayer.Play();
    }

    public void PlayVideo(int index)
    {
        var vInfo = loadedVideos[index];
        videoPlayer.clip = vInfo.clip;
        videoPlayer.Play();
        Debug.Log("Called Video" + index);
    }
}
