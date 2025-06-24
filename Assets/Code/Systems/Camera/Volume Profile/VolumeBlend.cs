using UnityEngine.Rendering.Universal;

namespace UnityEngine.Rendering
{
    [RequireComponent(typeof(Volume))]
    public class VolumeBlend : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float _weigth;

        [Header("Blend Effects")]
        [SerializeField] private FilmGainBlend _filmGain;
        [SerializeField] private VignetteBlend _vignette;
        [SerializeField] private ColorAdjustmentsBlend _colorAdjustments;

        private Volume _globalVolume;

        private void Awake()
        {
            _globalVolume = GetComponent<Volume>();
            if (!_globalVolume.profile) return;

            _filmGain.SetUp(_globalVolume);
            _vignette.SetUp(_globalVolume);
            _colorAdjustments.SetUp(_globalVolume);
        }
        private void Start() => UpdateBlend();
        private void OnValidate() => UpdateBlend();

        public void SetValue(float value) { _weigth = value; UpdateBlend(); }
        private void UpdateBlend()
        {
            if (!Application.isPlaying || !_globalVolume) return;

            _filmGain?.Set(_weigth);
            _vignette?.Set(_weigth);
            _colorAdjustments?.Set(_weigth);
        }
    }
}