using Unity.Mathematics;
using UnityEngine.Events;

namespace UnityEngine.InputSystem
{
    public class ScreenDraw : DragBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LineRenderer _render;

        [SerializeField, Range(0, 1)] private float _distance;
        [SerializeField] private int _distanceLimit;

        [SerializeField] private UnityEvent<float> _onUpdateDistance;

        private Vector2 _lastPosition;
        private float _current, _limit;

        protected override void Start()
        {
            float screenWidth = GetComponentInParent<Canvas>().GetComponent<RectTransform>().rect.width;
            float scaleFactor = screenWidth / 1920f;
            _limit = _distanceLimit * scaleFactor;
            base.Start();
            ClearTrack();
        }
        protected override void OnSelect()
        {
            if (IsPointerOverUI()) return;
            ClearTrack(); ForceUpdate();
            base.OnSelect();
        }
        protected override void OnUpdateSelection(float2 screenPosition)
        {
            if (_current >= _limit) return;

            Vector2 input = math.clamp(screenPosition, mathf.zero, new float2(Screen.width, Screen.height));
            Vector2 worldPos = _camera.ScreenToWorldPoint(input);
            Vector2 direction = _lastPosition - worldPos;

            if (direction.magnitude < _distance) return;
            if (_lastPosition != Vector2.zero) _current += direction.magnitude;

            _render.positionCount++;
            _render.SetPosition(_render.positionCount - 1, worldPos);
            _onUpdateDistance.Invoke(1f - (_current / _limit));
            _lastPosition = worldPos;
        }

        public void ClearTrack()
        {
            _onUpdateDistance.Invoke(1f);
            _lastPosition = Vector2.zero;
            _render.positionCount = 0;
            _current = 0;
        }
    }
}