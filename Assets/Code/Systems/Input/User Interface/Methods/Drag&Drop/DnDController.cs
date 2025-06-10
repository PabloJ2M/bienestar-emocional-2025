using System.Collections;
using UnityEngine.Events;

namespace UnityEngine.InputSystem
{
    public class DnDController : MonoBehaviour
    {
        [SerializeField] private RectTransform _container;
        [SerializeField] private ScreenManager _screen;
        [SerializeField] private OverlayArea _area;

        [SerializeField, Range(0, 1)] private float _delayEffect;
        [SerializeField] private UnityEvent<DnDElement> _onDropItem;

        private WaitForSeconds _delayTime;
        private RectTransform _parent;
        private DnDElement _element;

        public ScreenManager Screen => _screen;
        private void Start() => _delayTime = new(_delayEffect);
        private void Reset() => _container = transform as RectTransform;
        private void Awake() => _parent = transform.parent as RectTransform;

        public void DragItem(DnDElement type) { if (_element) return; _element = type; type.SetInFront(_parent); }
        public void DropItem() => StartCoroutine(DropDelay(_area.IsOverArea()));

        private IEnumerator DropDelay(bool isCompleted)
        {
            if (isCompleted) { _onDropItem?.Invoke(_element); _element?.CompleteTask(); yield return _delayTime; }
            _element?.ResetSibling(_container);
            _element = null;
        }
    }
}