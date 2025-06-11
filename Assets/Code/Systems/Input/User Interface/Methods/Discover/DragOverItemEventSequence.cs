using UnityEngine.Events;

namespace UnityEngine.InputSystem
{
    [RequireComponent(typeof(DragOverItem))]
    public class DragOverItemEventSequence : MonoBehaviour
    {
        [SerializeField] private UnityEvent[] _onCompleteTask = new UnityEvent[1];
    
        private int _index;

        private void OnEnable() => GetComponent<DragOverItem>().onDiscoverItem += DetectItem;

        private void DetectItem()
        {
            if (_index >= _onCompleteTask.Length) return;

            _onCompleteTask[_index].Invoke();
            _index++;
        }
    }
}