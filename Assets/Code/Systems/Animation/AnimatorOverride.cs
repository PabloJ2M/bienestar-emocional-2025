using UnityEngine;

public class AnimatorOverride : MonoBehaviour
{
    [SerializeField] private int _interactionIndex;
    private PetSelectorGameplay _pet;

    private const string _defaulAnimation = "Movement";
    private const string _triggerID = "Interact";

    private const string _waitID = "animal_task (wait)";
    private const string _interactID = "animal_task (interact)";

    private void Awake() => _pet = PetSelectorGameplay.Instance;

    private void OnEnable()
    {
        if (_pet == null) return;

        AnimatorOverrideController controller = _pet.Controller as AnimatorOverrideController;
        controller[_interactID] = _pet.Animation.interactions[0];
        _pet.Animator.CrossFade(_waitID, 1);
    }

    private void OnDisable() { if (_pet != null) _pet.Animator.CrossFade(_defaulAnimation, 1); }
    public void TriggerInteraction() { if (_pet != null) _pet.Animator.SetTrigger(_triggerID); }
}