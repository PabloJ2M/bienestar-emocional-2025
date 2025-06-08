using Unity.Mathematics;
using UnityEngine.Events;

namespace UnityEngine.InputSystem
{
    public class ScreenRotateAround : DragBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private UnityEvent<float> _onValueChanged;

        private RectTransform _self;
        private float2 _center;

        protected override void Awake() { base.Awake(); _self = GetComponent<RectTransform>(); }
        protected override void OnSelect() { base.OnSelect(); _center = RectTransformUtility.WorldToScreenPoint(_camera, _self.position); }

        protected override void OnUpdateSelection(float2 screenPosition)
        {
            float2 direction = screenPosition - _center;

            float angle = math.atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle = (angle + 360) % 360;

            _self.localEulerAngles = new float3(0, 0, angle);
            _onValueChanged.Invoke(angle / 360f);
        }
    }
}