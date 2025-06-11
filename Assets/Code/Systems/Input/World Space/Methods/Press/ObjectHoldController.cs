using System;
using Unity.Mathematics;
using UnityEngine.Events;

namespace UnityEngine.InputSystem
{
    public class ObjectHoldController : MonoBehaviour
    {
        [SerializeField] private ScreenRaycaster _screen;

        [Header("Win Condition")]
        [SerializeField] private float _amount;
        [SerializeField] private UnityEvent _onCompletedTask;

        private ObjectHoldWorld _current;
        private int _count;

        public event Action<ObjectHoldWorld> onHoldChanged;

        private void OnEnable()
        {
            _screen.onHit += PerformeInteraction;
            _screen.onRelease += PerformeRelease;
        }

        public float3 GetPosition(float3 position) => _screen.WorldToScreenPoint(position + math.up());

        private void PerformeInteraction(RaycastHit hit)
        {
            if (hit.collider == null) return;
            if (!hit.collider.TryGetComponent(out _current)) return;

            onHoldChanged?.Invoke(_current);
            _current?.Press();
        }
        private void PerformeRelease()
        {
            if (_current == null) return;
            if (_current.IsCompleted) _count++;
            if (_count >= _amount) _onCompletedTask?.Invoke();

            onHoldChanged?.Invoke(null);
            _current?.Release();
            _current = null;
        }
    }
}