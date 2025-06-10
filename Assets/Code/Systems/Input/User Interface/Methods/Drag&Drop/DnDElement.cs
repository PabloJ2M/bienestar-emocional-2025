using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine.Events;

namespace UnityEngine.InputSystem
{
    public class DnDElement : DragBehaviour
    {
        [SerializeField] private UnityEvent _onSuccess;

        private DnDController _group;
        private RectTransform _transform;

        private int _siblingIndex;

        protected override void Awake()
        {
            base.Awake();
            _transform = transform as RectTransform;
            _siblingIndex = _transform.GetSiblingIndex();
            _group = GetComponentInParent<DnDController>();
        }

        protected override void OnSelect() { base.OnSelect(); _group.DragItem(this); ForceInput(); }
        protected override void OnDeselect() { base.OnDeselect(); _group.DropItem(); }

        protected override void OnUpdateSelection(float2 input) => _transform.localPosition = _group.Screen.RectPos(input);
        private async void ForceInput() { await Task.Yield(); ForceUpdate(); }
        public void CompleteTask() => _onSuccess.Invoke();

        public void ResetSibling(Transform parent)
        {
            _transform.SetParent(parent);
            _transform.SetSiblingIndex(_siblingIndex);
        }
        public void SetInFront(Transform parent)
        {
            _transform.SetParent(parent);
            _transform.SetAsLastSibling();
        }
    }
}