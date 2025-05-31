using UnityEngine.EventSystems;

namespace UnityEngine.InputSystem
{
    public abstract class TouchBehaviour : InteractionBehaviour, IPointerDownHandler
    {
        protected bool _isOverElement;
        protected bool _isSelected;

        protected virtual void Start() => _inputs.UI.Click.performed += OnSelectStatus;
        protected virtual void OnDestroy() => _inputs.UI.Click.performed -= OnSelectStatus;

        protected void OnSelectStatus(InputAction.CallbackContext ctx)
        {
            bool isPressed = ctx.control.IsPressed();
            if (isPressed || !_isSelected) return;

            _isSelected = false;
            OnDeselect();
        }
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            _isSelected = true;
            OnSelect();
        }

        protected abstract void OnSelect();
        protected abstract void OnDeselect();

        public void ForceDisable() => _isSelected = false;
    }
}