using System;
using UnityEngine;

[CreateAssetMenu(fileName = "pet", menuName = "pets/pet type", order = 1)]
public class SO_Pet : ScriptableObject
{
    [SerializeField] private Sprite _bannet;

    [SerializeField] private PetAnimation _young;
    [SerializeField] private PetAnimation _normal;
    [SerializeField] private PetAnimation _old;

    private const string _ageID = "Age";

    public Sprite Banner => _bannet;
    public PetAnimation GetAge() => PlayerPrefs.GetInt(_ageID) switch
    {
        (> 20) => _old,
        (> 10) => _normal,
        _ => _young
    };
}

[Serializable] public struct PetAnimation
{
    public RuntimeAnimatorController controller;
    public AnimationClip[] interactions;
}