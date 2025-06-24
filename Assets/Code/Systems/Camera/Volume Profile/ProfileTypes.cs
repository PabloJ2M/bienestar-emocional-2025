using System;
using Unity.Mathematics;

namespace UnityEngine.Rendering
{
    using Universal;

    #region Constructors
    public abstract class VolumeType<T> where T : VolumeComponent
    {
        protected T _reference;

        public void SetUp(Volume volume) => volume.profile.TryGet(out _reference);
    }
    public abstract class NumberType<T> : VolumeType<T> where T : VolumeComponent
    {
        public abstract void Set(float value);
    }
    #endregion

    [Serializable] public class FilmGainBlend : NumberType<FilmGrain>
    {
        [SerializeField, Range(0f, 1f)] private float _min, _max;
        public override void Set(float value) => _reference.intensity.value = math.lerp(_min, _max, 1f - value);
    }
    [Serializable] public class VignetteBlend : NumberType<Vignette>
    {
        [SerializeField, Range(0f, 1f)] private float _min, _max;
        public override void Set(float value) => _reference.intensity.value = math.lerp(_min, _max, 1f - value);
    }
    [Serializable] public class ColorAdjustmentsBlend : NumberType<ColorAdjustments>
    {
        [SerializeField, Range(-100f, 100f)] private float _min, _max;
        public override void Set(float value) => _reference.saturation.value = math.lerp(_min, _max, value);
    }
}