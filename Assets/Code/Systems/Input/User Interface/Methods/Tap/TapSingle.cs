using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.InputSystem
{
    public class TapSingle : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private UnityEvent _onTap;
        private bool _isLocked;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isLocked) return;

            _onTap.Invoke();
            _isLocked = true;
            GetComponentInParent<TapMultiple>()?.OnTapElement();
        }
    }
}