namespace UnityEngine.InputSystem
{
    public class OverlayPlayer : MonoBehaviour
    {
        private ScreenRaycaster _caster;
        private RectTransform _transform;
        private Transform _player;

        private void Awake()
        {
            _transform = transform as RectTransform;
            _caster = GetComponentInParent<ScreenRaycaster>();
            _player = GameObject.FindWithTag("Player")?.transform;
        }
        private void Update()
        {
            if (_player == null) return;
            _transform.position = _caster.WorldToScreenPoint(_player.position + (Vector3.up * 0.75f));
        }
    }
}