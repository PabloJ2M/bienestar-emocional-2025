using UnityEngine;

public class PetSelectorGameplay : MonoBehaviour
{
    [SerializeField] private SO_PetList _list;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        int index = PetSelector.selected;
        var animation = _list.Pets[index].GetAge();

        _animator.runtimeAnimatorController = animation.controller;
    }
}