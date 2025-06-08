using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine.Animations
{
    public class TweenGroup : MonoBehaviour
    {
        [SerializeField] private bool _playOnAwake;
        [SerializeField, Range(0, 1)] private float _delay;

        [SerializeField] private bool _negateCallback;
        [SerializeField] private UnityEvent<bool> _onValueChanged;

        private List<ITween> tweens = new();

        private IEnumerator Start()
        {
            if (!_playOnAwake) yield break;
            yield return new WaitForEndOfFrame();
            EnableTween();
        }

        public void AddListener(ITween tween) => tweens.Add(tween);
        public void RemoveListener(ITween tween) => tweens.Remove(tween);

        [ContextMenu("Show")] public void EnableTween() => SetTweenStatus(true);
        [ContextMenu("Hide")] public void DisableTween() => SetTweenStatus(false);
        public void SetTweenStatus(bool value) => StartCoroutine(TweenDelay(value));

        private IEnumerator TweenDelay(bool value)
        {
            WaitForSeconds delay = new(_delay);
            foreach (var tween in tweens) { tween.Play(value); if (_delay != 0) yield return delay; }
            _onValueChanged.Invoke(_negateCallback ? !value : value);
        }
    }
}