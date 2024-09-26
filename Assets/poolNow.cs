//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class poolNow : MonoBehaviour
//{
//    public static poolNow Instance;
//    private List<GameObject> pooledObjects = new List<GameObject>();
//    [SerializeField]private int poolSize = 0;
//    [SerializeField] private GameObject EnemyPrefab;
//    private void Awake()
//    {
//        if (Instance == null) 
//        { 
//            Instance = this;
//        }
//    }

//    private void Start()
//    {
//        for (int i = 0; i < poolSize; i++) 
//        {
//            GameObject obj = Instantiate(EnemyPrefab);
//            obj.SetActive(false);
//            pooledObjects.Add(obj);
//        }
//    }

//    public GameObject pooledObjects()
//    {
//        for(int i = 0;i < pooledObjects; i++)
//        {
//            if (!pooledObjects[i].activeInHierarchy)

//        }
//    }

//}
