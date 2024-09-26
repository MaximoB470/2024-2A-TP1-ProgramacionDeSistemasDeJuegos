using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        private static Transform townCenter;

        public event Action OnSpawn = delegate { };
        public event Action OnDeath = delegate { };

        private void Reset() => FetchComponents();

        private void Awake()
        {
            FetchComponents();
            InitializeTownCenter();
        }

        private void FetchComponents()
        {
            agent ??= GetComponent<NavMeshAgent>();
        }

        private void InitializeTownCenter()
        {
            if (townCenter == null)
            {
                var townCenterObject = GameObject.FindGameObjectWithTag("TownCenter");
                //if (townCenter == null)
                //{
                //    Debug.LogError($"{name}: Found no {nameof(townCenter)}!! :(");
                //    return;
                //}

                townCenter = townCenterObject.transform;
            }
        }

        public void OnSpawnFromPool()
        {
            if (!agent.isActiveAndEnabled)
            {
                agent.enabled = true;
            }

            SetDestinationToTownCenter();
            StartCoroutine(AlertSpawn());
        }

        private void SetDestinationToTownCenter()
        {
           
            Vector3 destination = townCenter.position;
            destination.y = transform.position.y; 
            agent.SetDestination(destination);
        }
        private IEnumerator AlertSpawn()
        {
            yield return null;
            OnSpawn();
        }

        private void Update()
        {
            if (agent.hasPath && Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance)
            {
                Debug.Log($"{name}: I'll die for my people!");
                Die();
            }
        }

        private void Die()
        {
            OnDeath();
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            agent.enabled = false;
        }
    }
}
