using Unity.Mathematics;

namespace UnityEngine.InputSystem
{
    public abstract class DeltaBehaviour : DragBehaviour
    {
        private float2 _offset;

        protected override void OnDeselect() { base.OnDeselect(); _offset = float2.zero; }
        protected override void OnUpdateSelection(float2 delta)
        {
            if (Equals(_offset, float2.zero)) _offset = delta;

            float2 direction = _offset - delta;
            SetDirection(-direction);
            _offset = delta;
        }

        protected abstract void SetDirection(float2 direction);
    }
}