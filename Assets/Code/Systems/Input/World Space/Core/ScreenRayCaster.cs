using System;
using Unity.Mathematics;

namespace UnityEngine.InputSystem
{
    public class ScreenRayCaster : ActionsBehaviour
    {
        [SerializeField] private LayerMask _mask;
        private Camera _camera;

        public event Action<RaycastHit> onHit;
        public event Action onRelease;

        protected override void Awake() { base.Awake(); _camera = Camera.main; }
        protected virtual void Start() => _inputs.UI.Click.performed += RayDetection;
        public float3 WorldToScreenPoint(float3 position) => _camera.WorldToScreenPoint(position);

        private void RayDetection(InputAction.CallbackContext ctx)
        {
            if (!ctx.action.IsPressed()) { onRelease.Invoke(); return; }

            Ray ray = _camera.ScreenPointToRay(_inputs.UI.Point.ReadValue<Vector2>());
            Physics.Raycast(ray, out RaycastHit hit, math.INFINITY, _mask);
            onHit?.Invoke(hit);
        }
    }
}