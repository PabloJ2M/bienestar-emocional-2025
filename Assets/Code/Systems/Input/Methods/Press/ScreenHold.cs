using System;
using Unity.Mathematics;
using UnityEngine.Events;

namespace UnityEngine.InputSystem
{
    public class ScreenHold : TouchBehaviour
    {
        [SerializeField, Range(0, 1)] private float _speed;
        [SerializeField] private UnityEvent _onRelease;

        public Action<float> onValueChanged;
        private ScreenHoldUI _ui;

        private float _value;

        protected override void Awake() { base.Awake(); _ui = ScreenHoldUI.Instance; }
        protected override void OnEnable() { base.OnEnable(); onValueChanged += _ui.OnHold; }

        protected override void OnSelect()
        {
            _value = 0;
            _ui.OnPress();
            onValueChanged?.Invoke(_value);
        }
        protected override void OnDeselect()
        {
            _ui.OnRelease();
            _onRelease.Invoke();
            onValueChanged?.Invoke(_value);
        }

        private void FixedUpdate()
        {
            if (!_isSelected) return;

            _value = math.clamp(_value + _speed * Time.deltaTime, 0f, 1f);
            onValueChanged?.Invoke(_value);
        }
    }
}