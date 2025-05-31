using Unity.Mathematics;

namespace UnityEngine.InputSystem
{
    public abstract class DragBehaviour : TouchBehaviour
    {
        protected float2 _input;
        protected bool _isDragging;

        protected override void Start() { base.Start(); _inputs.UI.Point.performed += OnPointerUpdate; }
        protected override void OnDestroy() { base.OnDestroy(); _inputs.UI.Point.performed -= OnPointerUpdate; }

        protected void OnPointerUpdate(InputAction.CallbackContext ctx)
        {
            if (!_isDragging) return;

            _input = ctx.ReadValue<Vector2>();
            OnUpdateSelection(_input);
        }

        protected void ForceUpdate() => OnUpdateSelection(_inputs.UI.Point.ReadValue<Vector2>());
        protected abstract void OnUpdateSelection(float2 screenPosition);

        protected override void OnSelect() => _isDragging = true;
        protected override void OnDeselect() { _isDragging = false; _input = float2.zero; }
    }
}