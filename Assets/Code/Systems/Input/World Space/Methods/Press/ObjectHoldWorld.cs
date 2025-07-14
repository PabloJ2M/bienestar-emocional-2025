using Unity.Mathematics;

namespace UnityEngine.InputSystem
{
    public class ObjectHoldWorld : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particles;
        private bool _isPressed;
        private float _current;

        public bool IsCompleted => _current >= 1f;
        public float Value => _current;
        public float3 Position => transform.position;

        public void Press() => _isPressed = true;
        public void Release() { _isPressed = false; _current = 0; }

        private void Update()
        {
            if (!_isPressed) return;

            _current = math.clamp(_current + Time.deltaTime, 0f, 1f);
            
            if (!IsCompleted) return;

            Instantiate(_particles, transform.position, default);
            gameObject.SetActive(false);
        }
    }
}