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
            if (_overrideSource) PlaySound(clip);
            else _controller.PlayEffect(clip);
        }
        private void PlaySound(AudioClip clip)
        {
            _overrideSource.pitch = Random.Range(0.95f, 1.05f);
            _overrideSource.PlayOneShot(clip);
        }
    }
}