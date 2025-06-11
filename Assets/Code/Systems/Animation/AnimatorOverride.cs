using UnityEngine;

public class AnimatorOverride : MonoBehaviour
{
    [SerializeField] private Game _gameType;

    private PetSelectorGameplay _pet;
    private SO_PetAnimation _animation;

    private const string _defaulAnimation = "Movement";
    private const string _triggerID = "Interact";

    private const string _waitID = "animal_task (wait)";
    private const string _interactID = "animal_task (interact)";

    private void Awake() => _pet = PetSelectorGameplay.Instance;
    private void Start() { if (_pet) SetWaitAction(); }
    private void OnEnable() { if (_pet) _animation = _pet.Animation.interaction.Find(x => x.GameType == _gameType); }
    private void OnDisable() { if (_pet) _pet.Animator.CrossFade(_defaulAnimation, 1); }

    public void SetWaitAction(int index = 0)
    {
        if (_animation == null) return;

        AnimatorOverrideController controller = _pet.Controller as AnimatorOverrideController;
        controller[_waitID] = _animation.GetWaitByIndex(index);
        controller[_interactID] = _animation.GetActionByIndex(index);
        _pet.Animator.CrossFade(_waitID, 1);
    }
    public void TriggerInteraction() { if (_pet != null) _pet.Animator.SetTrigger(_triggerID); }
}