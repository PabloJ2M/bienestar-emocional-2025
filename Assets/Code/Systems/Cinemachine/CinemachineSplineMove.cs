using Unity.Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class CinemachineSplineMove : MonoBehaviour
{
    [SerializeField, Range(0, 5)] private float _height;
    [SerializeField, Range(0, 1)] private float _horizontalSpeed;
    private CinemachineSplineDolly _dolly;

    private void Awake() => _dolly = GetComponent<CinemachineSplineDolly>();
    private void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        Vector3 top = pos; top.y += _height;
        Vector3 bottom = pos; bottom.y -= _height;
        Gizmos.color = Color.blue; Gizmos.DrawLine(top, bottom);
    }

    public void Displace(Vector3 direction)
    {
        _dolly.CameraPosition += direction.x * _horizontalSpeed;

        var offset = _dolly.SplineOffset;
        offset.y = math.clamp(offset.y + direction.y, -_height, _height);
        _dolly.SplineOffset = offset;
    }
}