using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioRepeat : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    [SerializeField] AudioEmitter emitter;

    [SerializeField] float interval;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        emitter = GetComponent<AudioEmitter>();   
        StartCoroutine(PlayAudio());
    }


    IEnumerator PlayAudio()
    {

        emitter.PlayEffect(audio.clip);

        yield return new WaitForSeconds(interval);

        StartCoroutine(PlayAudio());
    }
}
