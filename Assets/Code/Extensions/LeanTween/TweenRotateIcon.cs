using UnityEngine.Events;

namespace UnityEngine.Animations
{
    public class TweenRotateIcon : TweenRotate
    {
        [SerializeField] private Sprite _normal, _changed;
        [SerializeField] private UnityEvent<Sprite> _onIconChanged;

        protected override void Start() { base.Start(); _onIconChanged.Invoke(_normal); }
        protected override void OnUpdate(float value)
        {
            _onIconChanged.Invoke(value > 0.5f ? _changed : _normal);
        }
    }
}