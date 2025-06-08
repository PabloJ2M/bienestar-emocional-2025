using UnityEngine;
using UnityEngine.Events;

public class DiscoveryItem : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private UnityEvent _onSuccess;

    private RectTransform _transform;
    private Discover _discover;
    private bool _locked;

    private void Awake() => _discover = FindFirstObjectByType<Discover>();
    private void Start() => _transform = transform as RectTransform;
    private void OnEnable() => _discover.OnPositionChanged += OnFocus;

    private void OnFocus(Vector2 position)
    {
        Vector2 direction = position - (Vector2)_transform.position;
        if (_locked || direction.magnitude > _distance) return;

        _discover.FoundItem();
        _onSuccess.Invoke();
        _locked = true;
    }
}