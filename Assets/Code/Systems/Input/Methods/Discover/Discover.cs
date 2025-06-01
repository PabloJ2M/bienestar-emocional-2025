using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Discover : DragBehaviour
{
    [SerializeField] private ScreenManager _screen;
    [SerializeField] private int _amount;
    [SerializeField] private UnityEvent _onCompleteTask;

    private RectTransform _transform;
    private int _currentAmount;

    public Action<Vector2> OnPositionChanged;

    protected override void Awake() { base.Awake(); _transform = transform as RectTransform; }
    protected override void OnUpdateSelection(float2 screenPosition)
    {
        _transform.position = _screen.WorldPos(screenPosition);
        OnPositionChanged?.Invoke(_transform.position);
    }
    
    public void FoundItem()
    {
        _currentAmount++;
        if (_currentAmount >= _amount) _onCompleteTask.Invoke();
    }
}