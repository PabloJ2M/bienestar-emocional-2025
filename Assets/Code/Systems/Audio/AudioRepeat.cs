using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioRepeat : MonoBehaviour
{
    [SerializeField, Range(0, 5)] private float interval;
    private WaitForSeconds _delay;
    private AudioEmitter _emitter;

    private void Awake() { _delay = new(interval); _emitter = GetComponent<AudioEmitter>(); }
    private IEnumerator Start()
    {
        yield return _delay;
        _emitter.PlayEffectCurrent();
        StartCoroutine(Start());
    }
}