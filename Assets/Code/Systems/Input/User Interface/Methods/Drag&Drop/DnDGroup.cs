using UnityEngine;
using UnityEngine.Events;

public class DnDGroup : MonoBehaviour
{
    [SerializeField] private int _amount;
    [SerializeField] private UnityEvent _onComplete;
    private int _flip, _cure;

    public void OnFlip() => _flip++;
    public void OnCure()
    {
        _cure++;

        if (_cure >= _amount)
        {
            _onComplete.Invoke();
        }
    }
}