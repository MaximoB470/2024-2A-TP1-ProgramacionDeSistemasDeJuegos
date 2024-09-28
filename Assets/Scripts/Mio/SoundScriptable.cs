using Audio;
using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundScriptable", menuName = "ScriptableObjects/SoundScriptable", order = 0)]
public class SoundScriptable : ScriptableObject
{
    [SerializeField] public RandomContainer<AudioClipData> SpawnSounds;
    [SerializeField] public RandomContainer<AudioClipData> DeathSounds;
}
