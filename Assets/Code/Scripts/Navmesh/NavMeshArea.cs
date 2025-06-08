using UnityEngine;

public class NavMeshArea : MonoBehaviour
{
    [SerializeField] private float _alpha = 0.75f;
    [SerializeField] private Vector3 _size;
    private Transform _transform;

    public Vector3 Position => _transform.position + RandomPoint();
    public float With => _size.x * 0.5f;
    public float Height => _size.y * 0.5f;

    private void Awake() => _transform = transform;
    private void OnValidate() => _size.y = 0;
    private Vector3 RandomPoint() => new(Random.Range(-With, With), 0f, Random.Range(-Height, Height));

    private void OnDrawGizmos()
    {
        if (!_transform) Awake();

        Gizmos.color = new(0, 1, 0, _alpha);
        Gizmos.DrawCube(_transform.position, _size);
    }
}