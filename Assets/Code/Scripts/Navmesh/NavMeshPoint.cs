using UnityEngine;

public class NavMeshPoint : MonoBehaviour
{
    private Transform _transform;

    public Vector3 Position => _transform.position;

    private void Awake() => _transform = transform;

    private void OnDrawGizmos()
    {
        if (!_transform) Awake();

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_transform.position, 0.15f);
    }
}