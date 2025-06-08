using UnityEngine;

public class CameraPointer : MonoBehaviour
{
    private Transform _transform, _target;

    private void Awake() => _target = Camera.main.transform;
    private void Start() => _transform = transform;
    private void Update() => _transform?.LookAt(_target);
}