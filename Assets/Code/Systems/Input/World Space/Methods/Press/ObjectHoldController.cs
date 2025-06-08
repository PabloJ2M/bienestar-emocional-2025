using System;
using Unity.Mathematics;

namespace UnityEngine.InputSystem
{
    public class ObjectHoldController : MonoBehaviour
    {
        [SerializeField] private ScreenRayCaster _screen;
        private ObjectHoldWorld _current;

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
            onHoldChanged?.Invoke(null);
            _current?.Release();
            _current = null;
        }
    }
}