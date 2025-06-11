using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine.Events;

namespace UnityEngine.InputSystem
{
    public class Drag : DragBehaviour
    {
        [SerializeField] private UnityEvent _onDrop;
        private RectTransform _canvas, _parent;
        private RectTransform _transform;
        private Drop _drop;

        protected override void Awake()
        {
            base.Awake();
            _drop = GetComponentInParent<Drop>();
            _transform = transform as RectTransform;
            _parent = _transform.parent as RectTransform;
            _canvas = GetComponentInParent<Canvas>().transform as RectTransform;
        }

        protected async override void OnSelect() { base.OnSelect(); _transform.SetParent(_canvas); await Task.Yield(); ForceUpdate(); }
        protected override void OnUpdateSelection(float2 screenPosition) => _transform.position = (Vector2)screenPosition;

        protected override void OnDeselect()
        {
            base.OnDeselect();

            if (!_drop.IsOverArea(_transform.position)) { _transform.SetParent(_parent); return; }
            _onDrop.Invoke();
        }
    }
}