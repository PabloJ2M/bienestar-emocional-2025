using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "pet", menuName = "pets/pet type", order = 1)]
public class SO_Pet : ScriptableObject
{
    [SerializeField] private Sprite _bannet;
    [SerializeField] private bool _isLocked;

    [SerializeField] private PetAnimation _young;
    [SerializeField] private PetAnimation _normal;
    [SerializeField] private PetAnimation _old;

    private const string _ageID = "Age";

    public Sprite Banner => _bannet;
    public bool IsLocked => _isLocked;

    public PetAnimation SetAge(float value)
    {
        PlayerPrefs.SetFloat(_ageID, value);
        return GetAge();
    }
    public PetAnimation GetAge() => PlayerPrefs.GetFloat(_ageID) switch
    {
        //(> 0.7f) => _old,
        //(> 0.3f) => _normal,
        _ => _young
    };
}

[Serializable] public struct PetAnimation
{
    public RuntimeAnimatorController controller;
    public List<SO_PetAnimation> interaction;
}