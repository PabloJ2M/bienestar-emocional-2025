using Unity.Mathematics;

namespace UnityEngine.InputSystem
{
    public class ObjectHoldWorld : MonoBehaviour
    {
        private bool _isPressed;
        private float _current;

        public float Value => _current;
        public float3 Position => transform.position;

        public void Press() => _isPressed = true;
        public void Release() { _isPressed = false; _current = 0; }

        private void Update()
        {
            if (!_isPressed) return;

            _current = math.clamp(_current + Time.deltaTime, 0f, 1f);
            if (_current >= 1f) gameObject.SetActive(false);
        }
    }
}