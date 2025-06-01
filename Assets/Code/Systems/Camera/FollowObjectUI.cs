using UnityEngine;

public class FollowObjectUI : MonoBehaviour
{
    [SerializeField] private string _tag = "Player";
    [SerializeField] private Vector2 _offset;
    private Transform _target;

    private RectTransform _transform;
    private ScreenManager _screen;

    private void Awake()
    {
        _screen = ScreenManager.Instance;
        _transform = transform as RectTransform;
        _target = GameObject.FindWithTag(_tag)?.transform;
    }
    private void FixedUpdate()
    {
        _transform.localPosition = _screen.ScreenPos((Vector2)_target.position + _offset);
        _transform.localPosition.Set(_transform.localPosition.x, _transform.localPosition.y, 0);
    }
}