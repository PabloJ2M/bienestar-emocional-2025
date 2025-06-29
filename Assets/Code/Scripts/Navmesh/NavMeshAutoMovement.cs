using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAutoMovement : MonoBehaviour
{
    [Header("NavMesh Settings")]
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField, Range(0, 10)] private float _waitingTime = 2f;

    [Header("World Center")]
    [SerializeField] private Vector3 _origin;
    [SerializeField] private bool _goToCenter;

    [Header("Random Movement Points")]
    [SerializeField] private NavMeshPoint[] _points;

    private WaitForSeconds _waitDelay;
    private WaitUntil _reachTarget;
    private int _lastPointIndex = -1;

    private void Awake()
    {
        _waitDelay = new WaitForSeconds(_waitingTime);
        _agent.updateUpAxis = _agent.updateRotation = false;
        _reachTarget = new(() => !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance);
    }

    private void Start() => StartCoroutine(AutoMoveRoutine());

    private IEnumerator AutoMoveRoutine()
    {
        while (true)
        {
            yield return _reachTarget;
            yield return _waitDelay;

            MoveToRandomPoint();
        }
    }

    private void MoveToRandomPoint()
    {
        int index;

        do {
            index = Random.Range(0, _points.Length);
        } while (index == _lastPointIndex && _points.Length > 1);

        _lastPointIndex = index;

        Vector3 targetPos = _points[index].Position;

        if (NavMesh.SamplePosition(targetPos, out NavMeshHit hit, 20f, NavMesh.AllAreas))
            _agent.SetDestination(hit.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_origin, 0.1f);
    }

    public void Stop()
    {
        if (!_goToCenter) return;
        StopAllCoroutines();
        _agent.SetDestination(_origin);
    }

    public void Play()
    {
        if (!_goToCenter) return;
        StartCoroutine(AutoMoveRoutine());
    }
}