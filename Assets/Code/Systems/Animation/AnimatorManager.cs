using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorManager : MonoBehaviour
{
    private Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();

    public void PlayAnimation(AnimationClip clip) => _animator.CrossFade(clip.name, 1);
    public void SetTrigger(string trigger) => _animator.SetTrigger(trigger);
}
