using UnityEngine;

public class PetSelectorGameplay : SingletonBasic<PetSelectorGameplay>
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SO_PetList _list;

    private PetAnimation _animation;

    public Animator Animator => _animator;
    public PetAnimation Animation => _animation;
    public RuntimeAnimatorController Controller { get => _animator.runtimeAnimatorController; set => _animator.runtimeAnimatorController = value; }

    private void Start()
    {
        int index = PetSelector.selected;
        if (index < 0) index = 0;

        _animation = _list.Pets[index].GetAge();
        _animator.runtimeAnimatorController = _animation.controller;
    }
}