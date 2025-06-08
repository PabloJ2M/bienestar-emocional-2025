using Unity.Mathematics;

namespace UnityEngine.AI
{
    public class NavMeshAnimation : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _render;
        [SerializeField] private Animator _animator;

        private Transform _camera;
        private NavMeshAgent _agent;

        private void Awake()
        {
            _camera = Camera.main.transform;
            _agent = GetComponent<NavMeshAgent>();
        }

        private void FixedUpdate()
        {
            CalculateScreen(out float forward, out float sideway);

            if (sideway != 0) _render.flipX = sideway > 0;

            _animator.SetBool("isJump", _agent.isOnOffMeshLink);

            bool isHorizontal = math.abs(forward) > math.abs(sideway);
            _animator.SetFloat("speedX", isHorizontal ? math.clamp(sideway, -1f, 1f) : 0f);
            _animator.SetFloat("speedZ", !isHorizontal ? math.clamp(forward, -1f, 1f) : 0f);
        }
        private void CalculateScreen(out float forwardAmount, out float sidewaysAmount)
        {
            Vector3 forward = _camera.forward; forward.y = 0; forward.Normalize();
            Vector3 right = _camera.right; right.y = 0; right.Normalize();

            forwardAmount = Vector3.Dot(_agent.velocity, forward);
            sidewaysAmount = Vector3.Dot(_agent.velocity, right);
        }
    }
}