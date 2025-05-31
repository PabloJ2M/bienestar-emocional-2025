namespace UnityEngine.InputSystem
{
    [RequireComponent(typeof(RectTransform))]
    public class OverlayArea : MonoBehaviour
    {
        private RectTransform _transform;

        private void Awake() => _transform = transform as RectTransform;

        public bool IsOverArea(Vector2 position) => _transform.rect.Contains(position);
    }
}