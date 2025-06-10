using UnityEngine;
using UnityEngine.Events;

public class CinemachineCameraFocus : MonoBehaviour
{
    [SerializeField] private UnityEvent<bool> _onStatusChanged;

    private void Start() => FocusOff();

    [ContextMenu("Focus")] public void FocusOn() => FocusStatus(false);
    [ContextMenu("UnFocus")] public void FocusOff() => FocusStatus(true);
    public void FocusStatus(bool value) => _onStatusChanged.Invoke(value);
}