using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class FeedType : DragBehaviour
{
    [SerializeField] private UnityEvent _onSuccess;

    private FeedController _controller;
    private RectTransform _transform;
    private int _siblingIndex;
    public float2 Position => _input;

    protected override void Awake()
    {
        base.Awake();
        _transform = transform as RectTransform;
        _siblingIndex = _transform.GetSiblingIndex();
        _controller = GetComponentInParent<FeedController>();
    }
    protected override void OnDeselect() { base.OnDeselect(); _controller.OnDropItem(); }
    protected override void OnUpdateSelection(float2 input) => _transform.position = _controller.Screen.WorldPos(input);
    protected async override void OnSelect() { base.OnSelect(); _controller.SelectItem(this); await Task.Yield(); ForceUpdate(); }

    public void ResetSibling() => _transform.SetSiblingIndex(_siblingIndex);
    public void CompleteTask() => _onSuccess.Invoke();
}
