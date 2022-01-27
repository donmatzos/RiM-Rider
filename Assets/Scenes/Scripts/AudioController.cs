using System.Collections;
using System.Collections.Generic;
using Scenes.Scripts;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource _audioSource;

    public AudioClip SoundWin;
    public AudioClip SoundCrash;
    public List<AudioClip> RimjobSounds;
    public List<AudioClip> RimmedSounds;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        GameManager.AudioController = this;
    }

    public void PlayAudio(AudioFiles audio)
    {
        if (_audioSource==null) return;
        switch (audio)
        {
            case AudioFiles.WIN:
                _audioSource.clip = SoundWin;
                _audioSource.Play();
                break;
            case AudioFiles.CRASH:
                _audioSource.clip = SoundCrash;
                _audioSource.Play();
                break;
            case AudioFiles.RIMJOB:
                _audioSource.clip = GetRandomAudioClip(RimjobSounds);
                _audioSource.Play();
                break;
            
            case AudioFiles.RIMMED:
                _audioSource.clip = GetRandomAudioClip(RimmedSounds);
                _audioSource.Play();
                break;
            
        }
        
    }

    private AudioClip GetRandomAudioClip(List<AudioClip> list)
    {
        return list[Random.Range(0, list.Count)];
    }
}
public enum AudioFiles
{
    WIN,
    CRASH,
    RIMJOB,
    RIMMED
}
