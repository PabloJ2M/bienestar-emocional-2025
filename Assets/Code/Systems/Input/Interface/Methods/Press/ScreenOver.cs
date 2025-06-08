using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ScreenOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private UnityEvent<bool> _isPointerOver;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isPointerOver.Invoke(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _isPointerOver.Invoke(false);
    }
}