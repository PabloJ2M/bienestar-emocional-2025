using System.Linq;
using UnityEngine;

public class RandomArea : MonoBehaviour
{
    [SerializeField] private float _threshold;
    [SerializeField] private Vector2Int _size;

    private Vector2Int[] _points;

    private void Awake() => _points = new Vector2Int[transform.childCount];
    private void Start()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            Vector2Int point;
            do { point = new(Random.Range(_size.x, -_size.x), Random.Range(_size.y, _size.y)); } while (!_points.Contains(point));

            transform.GetChild(i).position = new Vector3(point.x, 0, point.y) * _threshold;
            _points[i] = point;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new(0f, 1f, 0f, 0.25f);
        Gizmos.DrawWireCube(transform.position, new Vector3(_size.x, 0, _size.y) * _threshold);
    }
}