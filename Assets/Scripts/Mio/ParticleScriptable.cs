using System.Collections.Generic;
using UnityEngine;
using Enemies;

[CreateAssetMenu(fileName = "ParticleScriptable", menuName = "ScriptableObjects/ParticleScriptable", order = 1)]
public class ParticleScriptable : ScriptableObject
{
    [SerializeField] public RandomContainer<ParticleSystem> particles;
}