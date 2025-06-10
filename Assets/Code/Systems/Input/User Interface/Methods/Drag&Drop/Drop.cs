using Unity.Mathematics;
using UnityEngine.Events;

namespace UnityEngine.InputSystem
{
    public class Drop : MonoBehaviour
    {
        [SerializeField] private RectTransform _area;
        [SerializeField, Range(0, 10)] private int _amount;

        [SerializeField] private UnityEvent _onDropItem, _onCompletedTask;

        private int _current;

        public bool IsOverArea(float3 position)
        {
            bool isOver = RectTransformUtility.RectangleContainsScreenPoint(_area, position.xy);
            _onDropItem.Invoke();

            if (isOver) AddAmount();
            return isOver;
        }
        private void AddAmount()
        {
            _current++;
            if (_current >= _amount) _onCompletedTask.Invoke();
        }
    }
}