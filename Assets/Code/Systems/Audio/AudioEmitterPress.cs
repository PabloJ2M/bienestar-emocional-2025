using UnityEngine.EventSystems;

namespace UnityEngine.Audio
{
    public class AudioEmitterPress : AudioEmitter, IPointerClickHandler
    {
        [SerializeField] private AudioClip _clip;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_clip) PlayEffect(_clip);
        }
    }
}