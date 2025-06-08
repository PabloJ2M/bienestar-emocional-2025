using System.Collections;

namespace UnityEngine.AI
{
    public class NavMeshAutoMovement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField, Range(0, 10)] private float _waitingTime = 1f;
        
        [Header("World Center")]
        [SerializeField] private Vector3 _origin;
        [SerializeField] private bool _goToCenter;

        private WaitForSeconds _waitDelay;
        private WaitUntil _reachTarget;
        private NavMeshArea[] _points;

        private void Awake()
        {
            _waitDelay = new(_waitingTime);
            _points = GetComponentsInChildren<NavMeshArea>();
        }
        private void OnEnable()
        {
            _agent.updateUpAxis = _agent.updateRotation = false;
            _reachTarget = new(() => !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance);
        }

        private void MoveToRandomPoint() => _agent?.SetDestination(_points[Random.Range(0, _points.Length)].Position);
        private void OnDrawGizmosSelected() => Gizmos.DrawSphere(_origin, 0.1f);

        private IEnumerator Start()
        {
            yield return _reachTarget;
            yield return _waitDelay;
            MoveToRandomPoint();
            StartCoroutine(Start());
        }
        public void Stop()
        {
            if (!_goToCenter) return;
            StopAllCoroutines();
            _agent?.SetDestination(_origin);
        }
        public void Play()
        {
            if (!_goToCenter) return;
            StartCoroutine(Start());
        }
    }
}