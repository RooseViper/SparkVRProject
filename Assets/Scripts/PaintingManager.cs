using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingManager : MonoBehaviour
{
    [SerializeField] private Sprite[] touristPictures;
    private Painting[] paintings;
    public static PaintingManager Instance => _instance;
    private static PaintingManager _instance;
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        paintings = GetComponentsInChildren<Painting>();
    }

    public void LoadPaitnings()
    {
        if (GameManager.Instance.theme == GameManager.Theme.Tourism)
        {
            for (int i = 0; i < touristPictures.Length; i++)
            {
                paintings[i].SetPainting(touristPictures[i]);
            }
        }
    }
}
