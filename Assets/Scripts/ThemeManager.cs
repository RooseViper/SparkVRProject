using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    [SerializeField] private Transform teleportTransform;
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
        PlayerManager.Instance.playerRig.SetLocalPositionAndRotation(teleportTransform.position, Quaternion.Euler(teleportTransform.eulerAngles));
        toggleRays.ToList().ForEach(ray=> ray.DeactivateRay());
        audioSource.Play();
    }
}
