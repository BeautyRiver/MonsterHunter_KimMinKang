using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
  
    public AudioSource hitSource;
  
    private Dictionary<string, AudioSource> sounds = new Dictionary<string, AudioSource>();

    void Start()
    {      
        sounds["Hit"] = hitSource;      
    }

    public void PlaySound(string name)
    {
        if (sounds.ContainsKey(name))
        {
            sounds[name].Play();
        }
        else
        {
            Debug.LogWarning($"Sound {name} not found!");
        }
    }

    public void PlayOnShotSound(string name)
    {
        if (sounds.ContainsKey(name))
        {
            sounds[name].PlayOneShot(sounds[name].clip);
        }
        else
        {
            Debug.LogWarning($"Sound {name} not found!");
        }
    }


    public void StopSound(string name)
    {
        if (sounds.ContainsKey(name))
        {
            sounds[name].Stop();
        }
        else
        {
            Debug.LogWarning($"Sound {name} not found!");
        }
    }

    public void PlayLoopSound(string name)
    {
        if (sounds.ContainsKey(name) && !sounds[name].isPlaying)
        {
            sounds[name].loop = true;
            sounds[name].Play();
        }
    }

    public void StopLoopSound(string name)
    {
        if (sounds.ContainsKey(name) && sounds[name].isPlaying)
        {
            sounds[name].Stop();
        }
    }


}