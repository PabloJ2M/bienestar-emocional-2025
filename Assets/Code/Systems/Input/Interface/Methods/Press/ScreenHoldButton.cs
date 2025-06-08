using UnityEngine.UI;

namespace UnityEngine.InputSystem
{
    public class ScreenHoldButton : ScreenHold
    {
        [SerializeField] private Sprite _normal, _pressed;
        private Image _image;

        protected override void Awake() { base.Awake(); _image = GetComponent<Image>(); }
        protected override void OnSelect() { base.OnSelect(); _image.sprite = _pressed; }
        protected override void OnDeselect() { _image.sprite = _normal; base.OnDeselect(); }
    }
}