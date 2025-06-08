using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PetSelectorButton : MonoBehaviour
{
    [SerializeField] private Image _image;
    private Button _button;

    private void Awake() => _button = GetComponent<Button>();
    private void Start() => _button.onClick.AddListener(SetSelected);

    public void Setup(SO_Pet pet) => _image.sprite = pet.Banner;
    private void SetSelected() => PetSelector.selected = transform.GetSiblingIndex();
}