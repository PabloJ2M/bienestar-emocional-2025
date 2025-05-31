namespace UnityEngine.InputSystem
{
    [RequireComponent(typeof(RectTransform))]
    public class ScreenPoint : MonoBehaviour
    {
        private ScreenConnect _manager;
        private RectTransform _area;

        public Vector2 Position => _area.position;

        private void Awake()
        {
            _area = transform as RectTransform;
            _manager = GetComponentInParent<ScreenConnect>();
        }
        private void Update()
        {
            if (!_manager.IsDragging) return;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_area, _manager.Input, _manager.Camera, out Vector2 point))
            {
                if (!_area.rect.Contains(point)) return;
                _manager?.AddPoint(this);
            }
        }
    }
}