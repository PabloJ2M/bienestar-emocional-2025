namespace UnityEngine.Audio
{
    public class AudioEmitter : MonoBehaviour
    {
        [SerializeField] private AudioSource _overrideSource;
        private AudioController _controller;

        private void Awake() => _controller = AudioController.Instance;

        public void PlayEffectWithPitch(AudioClip clip, float pitch) => _controller.PlayEffect(clip, pitch);
        public void PlayEffect(AudioClip clip)
        {
            if (_overrideSource) _overrideSource.PlayOneShot(clip);
            else _controller.PlayEffect(clip);
        }
    }
}