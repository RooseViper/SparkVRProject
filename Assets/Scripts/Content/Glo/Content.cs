using System;
using UnityEngine;

namespace Content.Glo
{
   [Serializable]
   public class Content
   {
      public string title;
      public string year;
      [TextArea(5,10)]
      public string message;
      public Sprite sprite;
      public AudioClip audioClip;
   }
}
