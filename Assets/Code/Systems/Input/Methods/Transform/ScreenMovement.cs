using UnityEngine.Events;
using Unity.Mathematics;

namespace UnityEngine.InputSystem
{
    public class ScreenMovement : DeltaBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private bool _inverse;
        [SerializeField] private UnityEvent<Vector3> _onValueChanged;

        protected override void SetDirection(float2 direction)
        {
            Vector2 scroll = _inverse ? -direction : direction;
            _onValueChanged.Invoke(_speed * scroll * Time.deltaTime);
        }
    }
}