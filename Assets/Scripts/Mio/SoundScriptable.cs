using Audio;
using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundScriptable", menuName = "ScriptableObjects/SoundScriptable", order = 0)]
public class SoundScriptable : ScriptableObject
{
     public RandomContainer<AudioClipData> SpawnSounds;
     public RandomContainer<AudioClipData> DeathSounds;
}
