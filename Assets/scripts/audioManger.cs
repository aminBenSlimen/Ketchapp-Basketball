
using System;
using UnityEngine;

public class audioManger : MonoBehaviour
{
    public source[] source;


    private void Awake()
    {
        foreach(source s in source)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;
        }
    }
    public void Play(string name)
    {
        source s = Array.Find(source, sound => sound.name == name);
        if (s.audioSource == null)
            return;
        s.audioSource.Play();
    }
}
