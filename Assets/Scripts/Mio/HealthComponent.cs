using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HealthComponent
{
    public class HealthSystem 
    {
        public int life;
        public int maxHp;

        public void Awake()
        {
            life = maxHp;
        }
        public void GetDamage(int value)
        {
            life -= value;
        }
    }
} 