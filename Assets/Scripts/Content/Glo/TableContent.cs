using Nova;
using UnityEngine;

namespace Content.Glo
{
    public class TableContent : MonoBehaviour
    {
        [SerializeField]private TextBlock title;
        [SerializeField]private TextBlock year;
        [SerializeField]private TextBlock message;
        [SerializeField]private UIBlock2D displayUiBlock;
        [SerializeField]private AudioClip audioClip;
        private AudioSource audioSource;
        // Start is called before the first frame update
        private void Start()
        {
            audioSource = transform.parent.GetComponentInChildren<AudioSource>();
        }
        public void SetTitle(string text) => title.Text = text;
        public void SetYear(string text) => year.Text = text;
        public void SetMessage(string text) => message.Text = text;
        public void SetDisplayUiBlock(Sprite sprite) => displayUiBlock.SetImage(sprite);
        public void SetAudioClip(AudioClip clip) => audioClip = clip;
    }
}
