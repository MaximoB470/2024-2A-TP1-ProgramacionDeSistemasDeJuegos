using HealthComponent;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CommonStructure : MonoBehaviour
{
    public HealthSystem healthSystem;

    public event Action OnSpawn = delegate { };
    public event Action OnDeath = delegate { };
    private int TimeToSpawn = 20;
    private void InitializeHealthSystem(int maxHp)
    {
        healthSystem = new HealthSystem();
        healthSystem.maxHp = maxHp;
        healthSystem.Awake();  
    }

    public void Update()
    {
        
    }

    private void Awake()
    {  
        InitializeHealthSystem(300);  
    }

    public void TakeDamage(int damage)
    {
        healthSystem.GetDamage(damage);
    }
    private void Die()
    {
        OnDeath();
        gameObject.SetActive(false);
        StartCoroutine(SpawnCycle());
    }
    private void OnDisable()
    {
        enabled = false;
    }

    private IEnumerator SpawnCycle()
    {
        while (true) 
        {
            yield return new WaitForSeconds(TimeToSpawn);
            gameObject.SetActive(true);
            StopAllCoroutines();
        }
    }
}

