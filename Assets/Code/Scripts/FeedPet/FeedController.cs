using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FeedController : MonoBehaviour
{
    [SerializeField] private RectTransform _container;
    [SerializeField] private ScreenManager _screen;
    [SerializeField] private OverlayArea _area;

    private WaitForSeconds _delay = new(1f);
    private RectTransform _parent;
    private FeedType _selected;

    public ScreenManager Screen => _screen;
    private void Reset() => _container = transform as RectTransform;
    private void Awake() => _parent = transform.parent as RectTransform;

    public void SelectItem(FeedType type) { _selected = type; type.transform.SetParent(_parent); type.transform.SetAsLastSibling(); }
    public void OnDropItem() => StartCoroutine(RefillDelay(_area.IsOverArea(_selected.Position)));

    private IEnumerator RefillDelay(bool useDelay)
    {
        if (useDelay) { _selected.CompleteTask(); yield return _delay; }
        _selected.transform.SetParent(_container);
        _selected.ResetSibling();
    }
}