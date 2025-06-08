using UnityEngine;
using UnityEngine.Events;

public class CinemachineCameraFocus : MonoBehaviour
{
    [SerializeField] private UnityEvent _onFocus, _onUnFocus;
    [SerializeField] private UnityEvent<bool> _onStatusChanged;

    private void Start() => FocusOff();

    [ContextMenu("Focus")]
    public void FocusOn() => FocusStatus(false);

    [ContextMenu("UnFocus")]
    public void FocusOff() => FocusStatus(true);

    public void FocusStatus(bool value)
    {
        _onStatusChanged.Invoke(value);

        if (value) _onUnFocus.Invoke();
        else _onFocus.Invoke();
    }
}