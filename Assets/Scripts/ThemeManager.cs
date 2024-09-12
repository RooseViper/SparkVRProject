using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;

public class ThemeManager : MonoBehaviour
{
    [SerializeField] private VideoScreen[] videoScreens;
    [SerializeField] private VideoPlayer[] constantVideoPlayers;
    [SerializeField] private VideoClip[] constantPoliticalClips, constantTourismClips;
    [SerializeField] private ToggleRay[] toggleRays; 
    private AudioSource audioSource;
    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        toggleRays.ToList().ForEach(ray=> ray.ActivateRay());
    }

    public void Teleport(bool isPolitical)
    {
        GameManager.Instance.theme = isPolitical ? GameManager.Theme.Political : GameManager.Theme.Tourism;
        PaintingManager.Instance.LoadPaitnings();
        videoScreens.ToList().ForEach(vScreen=> vScreen.SetVideo(isPolitical));
        for (var i = 0; i < constantVideoPlayers.Length; i++)
        {
            constantVideoPlayers[i].clip = isPolitical ? constantPoliticalClips[i] : constantTourismClips[i];
        }
        PlayerManager.Instance.Teleport();
        toggleRays.ToList().ForEach(ray=> ray.DeactivateRay());
        audioSource.Play();
    }


}
