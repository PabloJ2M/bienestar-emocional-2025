using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private void Awake() { if (!_camera) _camera = Camera.main; }

    public Vector2 WorldPos(Vector2 input) => _camera.ScreenToWorldPoint(input);
}