using System;
using System.Collections.Generic;
using UnityEngine;

namespace Content.Glo
{
    public class ContentManager : MonoBehaviour
    {
        [SerializeField]private List<ContentType> contentTypes;
        [SerializeField] private string type;
        // Start is called before the first frame update
        private void Start()
        {
            var tableContents = FindObjectsOfType<TableContent>();
            var chosenContent = contentTypes.Find(cType => cType.type == type);
            for (var i = 0; i < tableContents.Length; i++)
            {
                tableContents[i].SetTitle(chosenContent.contents[i].title);
                tableContents[i].SetYear(chosenContent.contents[i].year);
                tableContents[i].SetMessage(chosenContent.contents[i].message);
                tableContents[i].SetDisplayUiBlock(chosenContent.contents[i].sprite);
                tableContents[i].SetAudioClip(chosenContent.contents[i].audioClip);
            }
        }
    }
}
