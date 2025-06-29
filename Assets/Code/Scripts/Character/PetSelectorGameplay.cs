using UnityEngine;

public class PetSelectorGameplay : SingletonBasic<PetSelectorGameplay>
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SO_PetList _list;

    private PetAnimation _animation;
    private int _petIndex;

    public Animator Animator => _animator;
    public PetAnimation Animation => _animation;
    public RuntimeAnimatorController Controller { get => _animator.runtimeAnimatorController; set => _animator.runtimeAnimatorController = value; }

    protected override void Awake()
    {
        base.Awake();

        _petIndex = PetSelector.selected;
        if (_petIndex < 0) _petIndex = 0;

        _animation = _list.Pets[_petIndex].GetAge();
        _animator.runtimeAnimatorController = _animation.controller;
    }

    public void SetPetAge(float value)
    {
        _animation = _list.Pets[_petIndex].SetAge(value);
        _animator.runtimeAnimatorController = _animation.controller;
    }
}