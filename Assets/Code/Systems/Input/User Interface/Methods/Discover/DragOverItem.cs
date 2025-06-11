using System;
using Unity.Mathematics;

namespace UnityEngine.InputSystem
{
    public class DragOverItem : MonoBehaviour
    {
        public bool IsLocked => _locked;
        public float2 Position => (Vector2)_transform.position;

        public event Action onDiscoverItem;

        private RectTransform _transform;
        private bool _locked;

        private void Start() => _transform = transform as RectTransform;
        private void OnEnable() => DragOver.OnCompleteTask += OnReset;
        private void OnDisable() => DragOver.OnCompleteTask -= OnReset;

        public void OnOverItem() { if (_locked) return; onDiscoverItem?.Invoke(); _locked = true; }
        private void OnReset() => _locked = false;
    }
}