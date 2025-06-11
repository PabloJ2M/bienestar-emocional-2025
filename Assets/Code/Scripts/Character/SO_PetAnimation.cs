using Unity.Mathematics;
using UnityEngine;

public enum Game { Feed, Shower }

[CreateAssetMenu(fileName = "pet animation", menuName = "pets/pet animation", order = 2)]
public class SO_PetAnimation : ScriptableObject
{
    [SerializeField] private Game _gameType;

    [SerializeField] private AnimationClip[] _waits;
    [SerializeField] private AnimationClip[] _actions;

    public Game GameType => _gameType;
    public AnimationClip GetWaitByIndex(int index) => _waits[math.clamp(index, 0, _waits.Length - 1)];
    public AnimationClip GetActionByIndex(int index) => _actions[math.clamp(index, 0, _actions.Length - 1)];
}