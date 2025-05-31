namespace UnityEngine.InputSystem
{
    public abstract class PointerBehaviour : ActionsBehaviour
    {
        protected virtual void Start() => _inputs.UI.Point.performed += OnPointer;
        protected virtual void OnDestroy() => _inputs.UI.Point.performed -= OnPointer;

        protected abstract void OnPointer(InputAction.CallbackContext ctx);
    }
}