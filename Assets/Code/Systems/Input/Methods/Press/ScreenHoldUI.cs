using UnityEngine.Events;
using UnityEngine.UI;

namespace UnityEngine.InputSystem
{
    public class ScreenHoldUI : SingletonBasic<ScreenHoldUI>
    {
        [SerializeField] private Image _loading;
        [SerializeField] private UnityEvent<bool> _onInteractChanged;

        public void OnHold(float value) => _loading.fillAmount = value;
        
        public void OnPress()
        {
            _onInteractChanged.Invoke(true);
        }
        public void OnRelease()
        {
            _onInteractChanged.Invoke(false);
        }
    }
}