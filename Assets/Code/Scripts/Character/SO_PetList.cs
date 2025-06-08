using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "pet list", menuName = "pets/pet list", order = 0)]
public class SO_PetList : ScriptableObject
{
    [SerializeField] private List<SO_Pet> _pets;

    public List<SO_Pet> Pets => _pets;
}