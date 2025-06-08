using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class PetSelector : MonoBehaviour
{
    [SerializeField] private SO_PetList _list;
    [SerializeField] private PetSelectorButton _prefab;
    [SerializeField] private UnityEvent _onSkipSelection;

    private ScrollRect _scroll;

    private const string _petID = "pet";
    public static int selected = -1;

    private void Awake() => _scroll = GetComponent<ScrollRect>();
    private void OnEnable() => selected = PlayerPrefs.GetInt(_petID, -1);
    private void OnDisable() => PlayerPrefs.SetInt(_petID, selected);

    private void Start()
    {
        if (selected >= 0) _onSkipSelection.Invoke();

        foreach (var pet in _list.Pets)
            Instantiate(_prefab, _scroll.content).Setup(pet);
    }
}