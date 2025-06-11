using UnityEngine.Events;

namespace UnityEngine.InputSystem
{
    public class TapMultiple : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onCompletedTask;
        private int _current, _amount;

        private void Awake() => _amount = transform.childCount;
        public void OnTapElement()
        {
            _current++;

            if (_current < _amount) return;
            _onCompletedTask.Invoke();
        }
    }
}