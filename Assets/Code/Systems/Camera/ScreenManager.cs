using UnityEngine;

public class ScreenManager : SingletonBasic<ScreenManager>
{
    [SerializeField] private Camera _camera;
    private RectTransform _canvas;

    protected override void Awake()
    {
        base.Awake();
        if (!_camera) _camera = Camera.main;
        _canvas = GetComponent<RectTransform>();
    }

    public Camera Camera => _camera;
    public Vector2 WorldPos(Vector2 input) => _camera.ScreenToWorldPoint(input);
    public Vector2 ScreenPos(Vector2 pos) => _camera.WorldToScreenPoint(pos);
    public Vector2 RectPos(Vector2 input)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas, input, _camera, out Vector2 pos);
        return pos;
    }
}