namespace UnityEngine.InputSystem
{
    [RequireComponent(typeof(RectTransform))]
    public class OverlayArea : ActionsBehaviour
    {
        private ScreenManager _screen;
        private RectTransform _transform;

        protected override void Awake()
        {
            base.Awake();
            _screen = ScreenManager.Instance;
            _transform = transform as RectTransform;
        }

        public bool IsOverArea()
        {
            Vector2 input = _inputs.UI.Point.ReadValue<Vector2>();
            return RectTransformUtility.RectangleContainsScreenPoint(_transform, input, _screen.Camera);
        }
    }
}