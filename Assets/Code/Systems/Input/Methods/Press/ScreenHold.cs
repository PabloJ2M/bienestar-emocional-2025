using UnityEngine.Events;

namespace UnityEngine.InputSystem
{
    public class ScreenHold : TouchBehaviour
    {
        [SerializeField, Range(0, 1)] private float _speed;

        [SerializeField] private UnityEvent _onRelease;
        [SerializeField] private UnityEvent<float> _onValueChange;

        private float _value;

        protected override void OnSelect() { _value = 0; _onValueChange.Invoke(_value); }
        protected override void OnDeselect() { _onValueChange.Invoke(_value); _onRelease.Invoke(); }

        private void FixedUpdate()
        {
            if (!_isSelected) return;

            _value += _speed * Time.deltaTime;
            _onValueChange.Invoke(_value);
        }
    }
}