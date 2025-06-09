using UnityEngine;

public class StatSender : MonoBehaviour
{
    [SerializeField] private StatType _types;
    [SerializeField, Tooltip("scale of 100")] private float _amount;

    private GameController _controller;

    private void Awake() => _controller = GameController.Instance;
    public void AddValue() => _controller?.AddAmount(_types, _amount / 100f);
}