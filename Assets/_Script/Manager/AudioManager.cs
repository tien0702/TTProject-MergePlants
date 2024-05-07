using UnityEngine;
using System;
using System.IO;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { private set; get; }

    [SerializeField] private Sound[] SFXSounds;
    [SerializeField] private Sound[] MusicSounds;

    [SerializeField] private AudioSource MusicSource;
    [SerializeField] private AudioSource SFXSource;


    void Awake()
    {
        MusicSource = transform.Find("Music_Source").GetComponent<AudioSource>();
        SFXSource = transform.Find("SFX_Source").GetComponent<AudioSource>();
        if (Instance == null || Instance != this)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        MusicSource.volume = 1;
        SFXSource.volume = 1;

        MusicSource.mute = false;
        SFXSource.mute = false;
        PlayMusic("BGM1", true);

    }

    public void PlayMusic(string name, bool loop)
    {
        Sound sound = Array.Find(MusicSounds, s => s.name == name);
        if (sound == null)
        {
            Debug.Log(string.Format("Sound {0} Not Found", name));
        }
        else
        {
            MusicSource.clip = sound.clip;
            MusicSource.loop = loop;
            MusicSource.Play();
        }
    }

    public void StopMusic(string name)
    {
        Sound sound = Array.Find(MusicSounds, s => s.name == name);
        if (sound == null)
        {
            Debug.Log(string.Format("Sound {0} Not Found", name));
        }
        else
        {
            MusicSource?.Stop();
        }
    }

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(SFXSounds, s => s.name == name);
        if (sound == null)
        {
            Debug.Log(string.Format("SFX {0} Not Found", name));
        }
        else
        {
            SFXSource.PlayOneShot(sound.clip);
        }
    }
}
