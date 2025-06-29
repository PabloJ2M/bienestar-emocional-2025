using Unity.Mathematics;

namespace UnityEngine.AI
{
    public class NavMeshAnimation : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _render;
        [SerializeField] private Animator _animator;

        private NavMeshAgent _agent;
        private Transform _transform, _camera;
        private Vector3 _lastPosition;

        private void Awake()
        {
            _transform = transform;
            _camera = Camera.main.transform;
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            CalculateScreen(out float forward, out float sideway);
            if (sideway != 0) _render.flipX = sideway > 0;

            _animator.SetFloat("speedX", math.clamp(sideway, -1f, 1f));
            _animator.SetFloat("speedZ", math.clamp(forward, -1f, 1f));

            _animator.SetBool("isJump", _agent.isOnOffMeshLink);
        }
        private void CalculateScreen(out float forwardAmount, out float sidewaysAmount)
        {
            float3 delta = _transform.position - _lastPosition; delta.y = 0;
            _lastPosition = _transform.position;

            float3 forward = _camera.forward; forward.y = 0;
            float3 right = _camera.right; right.y = 0;

            if (!Equals(delta, float3.zero)) delta = math.normalize(delta);
            forwardAmount = math.dot(delta, math.normalize(forward));
            sidewaysAmount = math.dot(delta, math.normalize(right));
        }
    }
}