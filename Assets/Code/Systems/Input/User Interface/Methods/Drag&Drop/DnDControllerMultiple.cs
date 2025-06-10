using UnityEngine.Events;

namespace UnityEngine.InputSystem
{
    public class DnDControllerMultiple : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onFlip, _onCure;
        private bool _isFliped;

        public void FlipAnimation()
        {
            _onFlip.Invoke();
            _isFliped = true;
        }
        public void ExcecuteAction()
        {
            if (!_isFliped) return;
            _onCure.Invoke();
        }
    }
}