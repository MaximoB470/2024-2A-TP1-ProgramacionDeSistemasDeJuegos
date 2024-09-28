using Audio;
using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AudioService : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private SoundScriptable DeathSoundFiles;
    private void Awake()
    {
        ServiceLocator.Instance.SetService("AudioService", this);
    }

    public RandomContainer<AudioClipData> DeathFiles()
    {
        return DeathSoundFiles.DeathSounds;
    }  
}