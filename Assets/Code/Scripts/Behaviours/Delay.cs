using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Events.Interact
{
    public class Delay : MonoBehaviour
    {
        [SerializeField] private float _defaultTime;
        [SerializeField] private bool _playOnAwake = false;

        [SerializeField] private UnityEvent _onStart, _onComplete;

        private WaitForEndOfFrame _waitForEndOfFrame = new();
        private bool _isPlaying = false;
        private float _current = 0;

        public bool IsPlaying => _isPlaying;

        public void Start() { if (_playOnAwake) Play(); }

        public void Play(float value = 1) { _current = value; Stop(); StartCoroutine(Timer(value)); }
        public void Play() => Play(_defaultTime);
        public void Stop() { StopAllCoroutines(); _isPlaying = false; }

        private IEnumerator Timer(float time)
        {
            if (_isPlaying) yield break;

            _current = time; _isPlaying = true;
            _onStart.Invoke();

            while (_current > 0)
            {
                yield return _waitForEndOfFrame;
                _current -= Time.deltaTime;
            }

            _isPlaying = false;
            _onComplete.Invoke();
        }
    }
}