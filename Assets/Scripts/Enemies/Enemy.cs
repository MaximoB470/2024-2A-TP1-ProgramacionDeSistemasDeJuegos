using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using HealthComponent;

namespace Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        public HealthSystem healthSystem;

        [SerializeField] private NavMeshAgent agent;

        public NavMeshAgent Agent
        {
            get { return agent; }
            set { agent = value; }
        }

        private CommonStructure commonStructure; 

        private static Transform townCenter;
        private static Transform TownStructure;

        public event Action OnSpawn = delegate { };
        public event Action OnDeath = delegate { };
        private void InitializeHealthSystem(int maxHp)
        {
            healthSystem = new HealthSystem();
            healthSystem.maxHp = maxHp;
            healthSystem.Awake();  
        }

        private void Reset() => FetchComponents();

        private void Awake()
        {
            FetchComponents();
            InitializeTownCenter();
            InitializeHealthSystem(100);  
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
                if (townCenterObject == null)
                {
                    Debug.LogError($"{name}:no town center");
                    return;
                }
                townCenter = townCenterObject.transform;
            }
        }
        private void InitializeSearchForStructures()
        {
            if (TownStructure == null)
            {
                var townStructureObject = GameObject.FindGameObjectWithTag("TownStructure");
                if (townStructureObject == null)
                {
                    Debug.LogError($"{name}: no town structure");
                    return;
                }
                TownStructure = townStructureObject.transform;
            }
        }

        public void OnSpawnFromPool()
        {
            if (!agent.isActiveAndEnabled)
            {
                agent.enabled = true;
            }
            healthSystem.life = healthSystem.maxHp;
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
            MainPath();
        }

        public void MainPath()
        {
            if (agent.hasPath && Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance)
            {
                TakeDamage(1); 
                Debug.Log($"{name}: I'll die for my people!");
            }

            if (healthSystem.life <= 0)
            {
                Debug.Log($"{name}: I'm dead :(");
                Die();
            }
        }
        public void TakeDamage(int damage)
        {
             healthSystem.GetDamage(damage);
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

        public GameObject Clone(Vector3 pos, Quaternion rot)
        {
            return Instantiate(this.gameObject, pos, rot);
        }
    }
}
