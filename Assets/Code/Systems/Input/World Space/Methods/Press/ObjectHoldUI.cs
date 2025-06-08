using UnityEngine.UI;
using UnityEngine.Animations;

namespace UnityEngine.InputSystem
{
    public class ObjectHoldUI : MonoBehaviour
    {
        [SerializeField] private ObjectHoldController _controller;
        [SerializeField] private TweenCore _fade;

        [SerializeField] private RectTransform _loading;
        [SerializeField] private Image _loadingBar;
        private ObjectHoldWorld _selected;

        private void OnEnable() => _controller.onHoldChanged += OnPerformeObject;
        private void OnPerformeObject(ObjectHoldWorld obj)
        {
            _fade.Play(obj != null);
            _selected = obj;
        }

        private void Update()
        {
            if (_selected == null) return;
            _loadingBar.fillAmount = _selected.Value;
            _loading.position = _controller.GetPosition(_selected.Position);
        }
    }
}