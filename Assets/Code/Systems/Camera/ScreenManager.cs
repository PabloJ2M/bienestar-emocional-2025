using UnityEngine;

public class ScreenManager : SingletonBasic<ScreenManager>
{
    [SerializeField] private Camera _camera;

    protected override void Awake() { base.Awake(); if (!_camera) _camera = Camera.main; }

    public Camera Camera => _camera;
    public Vector2 WorldPos(Vector2 input) => _camera.ScreenToWorldPoint(input);
    public Vector2 ScreenPos(Vector2 pos) => _camera.WorldToScreenPoint(pos);
}