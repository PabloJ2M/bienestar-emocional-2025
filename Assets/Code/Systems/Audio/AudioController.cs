namespace UnityEngine.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioController : Singleton<AudioController>
    {
        [SerializeField] private AudioSource _effects, _variable;

        public void PlayEffect(AudioClip clip) => _effects?.PlayOneShot(clip);
        public void PlayEffect(AudioClip clip, float pitch)
        {
            _variable.pitch = pitch;
            _variable?.PlayOneShot(clip);
        }
    }
}