using System;
using System.Linq;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine.Events;

namespace UnityEngine.InputSystem
{
    public class DragOver : DragBehaviour
    {
        [SerializeField] private RectTransform _parent;
        [SerializeField, Range(0, 100)] private float _distance;

        [SerializeField] private UnityEvent _onCompletedTask;

        private List<DragOverItem> _items;
        private RectTransform _transform;
        private int _current;

        public static event Action OnCompleteTask;

        protected override void Awake() { base.Awake(); _transform = transform as RectTransform; }
        protected override void Start() { base.Start(); _items = _parent.GetComponentsInChildren<DragOverItem>().ToList(); }
        protected override void OnUpdateSelection(float2 screenPosition) { _transform.position = (Vector2)screenPosition; DetectItem(screenPosition); }
        private void OnDrawGizmosSelected() => Gizmos.DrawWireSphere(transform.position, _distance);

        private void DetectItem(float2 position)
        {
            if (_current >= _items.Count) return;

            var item = _items.Find(x => !x.IsLocked && math.distance(position, x.Position) < _distance);
            if (item == null) return;

            item?.OnOverItem();
            FoundItem();
        }
        private void FoundItem()
        {
            _current++;
            if (_current < _items.Count) return;

            OnCompleteTask?.Invoke();
            _onCompletedTask.Invoke();
        }
    }
}