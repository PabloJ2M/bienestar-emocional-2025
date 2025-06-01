using UnityEngine.Events;

namespace UnityEngine.InputSystem
{
    public class HoldTarget : MonoBehaviour
    {
        [SerializeField] private ScreenHold _hold;
        [SerializeField] private float _target;
        [SerializeField] private UnityEvent _onSuccess, _onFailure;

        private float _current;

        private void OnEnable() => _hold.onValueChanged += SetValue;
        private void SetValue(float value) => _current = value;

        public void OnResult()
        {
            if (_current >= _target) _onSuccess.Invoke();
            else _onFailure.Invoke();
        }
    }
}