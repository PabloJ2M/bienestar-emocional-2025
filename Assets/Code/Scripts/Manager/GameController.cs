using System;
using UnityEngine;
using UnityEngine.Events;

[Flags]
public enum StatType { None = 0, Felicidad = 1, Alimento = 2, Limpieza = 4, Salud = 8 }

public class GameController : SingletonBasic<GameController>
{
    [SerializeField] private UnityEvent _onCenterPlayer, _onReleasePlayer;
    [SerializeField] private UnityEvent<bool> _focusCamera;

    public event Action<StatType, float> onValueChanged;

    public void AddAmount(StatType type, float amount) => onValueChanged?.Invoke(type, amount);
    public void GoToCenter(bool value) { if (value) _onCenterPlayer?.Invoke(); else _onReleasePlayer?.Invoke(); }
    public void CameraFocusStatus(bool value) => _focusCamera?.Invoke(value);
}