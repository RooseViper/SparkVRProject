using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class VideoScreen
{
    [SerializeField] private VideoManager videoManager;
    [SerializeField]private List<VideoInfo> politicalVideoInfos, touristVideoInfos;

    public void SetVideo(bool isPolitical)
    {
        videoManager.SetVideos(isPolitical ? politicalVideoInfos : touristVideoInfos);
    }
}
