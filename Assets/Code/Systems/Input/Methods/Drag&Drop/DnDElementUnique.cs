using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using UnityEngine.Events;

public class DnDElementUnique : DragBehaviour
{
    [SerializeField] private UnityEvent _onSuccess;
    private RectTransform _transform, _canvas, _parent;
    private ScreenManager _screen;
    private Collider2D _collider;

    protected override void Awake()
    {
        base.Awake();
        _screen = ScreenManager.Instance;
        _transform = transform as RectTransform;
        _parent = _transform.parent as RectTransform;
        _canvas = _parent.GetComponentInParent<Canvas>().transform as RectTransform;
    }

    protected override void OnUpdateSelection(float2 screenPosition) => _transform.localPosition = _screen.RectPos(screenPosition);
    protected override void OnSelect() { base.OnSelect(); RemoveParent(); ForceInput(); }
    protected override void OnDeselect()
    {
        base.OnDeselect();
        if (_collider == null) { ResetParent(); return; }
        if (_collider.TryGetComponent(out DnDControllerMultiple element)) { element.ExcecuteAction(); CompleteTask(); _onSuccess.Invoke(); }
        else ResetParent();
    }

    private async void ForceInput() { await Task.Yield(); ForceUpdate(); }

    public virtual void RemoveParent() { _transform.SetParent(_canvas); _transform.SetAsLastSibling(); }
    public virtual void ResetParent() => StartCoroutine(ResetDelay(false));
    public virtual void CompleteTask() => StartCoroutine(ResetDelay(true));

    private void OnTriggerEnter2D(Collider2D collision) { if (collision.CompareTag("Point")) _collider = collision; }
    private void OnTriggerExit2D(Collider2D collision) { if (collision.CompareTag("Point")) _collider = null; }

    private IEnumerator ResetDelay(bool hasDelay)
    {
        if (hasDelay) yield return new WaitForSeconds(1f);
        _transform.SetParent(_parent);
    }
}