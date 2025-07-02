using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PetSelectorButton : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private GameObject _lockedScreen;

    private Button _button;

    private void Awake() => _button = GetComponent<Button>();
    private void Start() => _button.onClick.AddListener(SetSelected);

    private void SetSelected() => PetSelector.selected = transform.GetSiblingIndex();
    public void Setup(SO_Pet pet)
    {
        _image.sprite = pet.Banner;
        _button.interactable = !pet.IsLocked;
        _lockedScreen.SetActive(pet.IsLocked);
    }
}