using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;

public class StatListener : MonoBehaviour
{
    [SerializeField] private StatType _type;
    [SerializeField] private Image _fill;

    private string _statID;
    private float _target;

    private void Awake() => _statID = $"stat_{_type}";
    private void Start() => _target = PlayerPrefs.GetFloat(_statID);
    private void OnEnable() => GameController.Instance.onValueChanged += PerformeStatus;
    private void OnDisable() => GameController.Instance.onValueChanged -= PerformeStatus;

    private void Update() { if (_fill != null) _fill.fillAmount = math.lerp(_fill.fillAmount, _target, Time.deltaTime); }

    private void PerformeStatus(StatType type, float amount)
    {
        if (!type.HasFlag(_type)) return;

        _target += amount;
        PlayerPrefs.SetFloat(_statID, _target);
    }
}