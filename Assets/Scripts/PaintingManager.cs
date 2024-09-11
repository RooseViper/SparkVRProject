using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingManager : MonoBehaviour
{
    [SerializeField] private Sprite[] touristPictures;
    [SerializeField] private Sprite[] politicalPictures;
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
            for (var i = 0; i < touristPictures.Length; i++)
            {
                paintings[i].SetPainting(touristPictures[i]);
            }
        }
        else
        {
            for (var i = 0; i < politicalPictures.Length; i++)
            {
                paintings[i].SetPainting(politicalPictures[i]);
            }
        }
    }
}
