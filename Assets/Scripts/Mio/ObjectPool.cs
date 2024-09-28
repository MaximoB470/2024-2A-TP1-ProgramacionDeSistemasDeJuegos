using Enemies;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/*Generador de pools con cola  
 
Bibliografia:
https://www.youtube.com/watch?v=YCHJwnmUGDk
https://www.youtube.com/watch?v=tdSmKaJvCoA
 */

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private IPrototype prototype;

    //singleton
    public static ObjectPool Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //singleton

    //serializamos editor de pools
    [System.Serializable]

    //Creamos Clase de Pool para crear las distintas Pools, nos ahorra definirlo por codigo
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    //Hacemos la lista en base a Pool
    public List<Pool> pools;

    //Diccionario de pool para guardar Objetos
    public Dictionary<string, Queue<GameObject>> poolDict;

    public void Start()
    {
        //Definimos el diccionario creado, listo para usar
        poolDict = new Dictionary<string, Queue<GameObject>>();

        //recorremos nuestros diccionarios y dentro de este, nuestros game objects
        foreach (Pool pool in pools)
        {
            //Cola
            Queue<GameObject> objectPool = new Queue<GameObject>();
            

            //quiero que recorra mi lista hasta que se llene
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                //acolamos objetos
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDict.Add(pool.tag, objectPool); //se lo damos a nuestro diccionario
        }
    }

    //objeto que vamos a spawnear/descolar
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        //Habilitamos objeto activandolo y asignando su posicion
        GameObject objectToSpawn = poolDict[tag].Dequeue();
        objectToSpawn.SetActive(true);
        //Hacemos Warp para que el enemigo no se vaya de su rumbo
        objectToSpawn.GetComponent<Enemy>().Agent.Warp(position);
        objectToSpawn.transform.rotation = rotation;
        //lo encolamos cuando lo dejamos de usar, para que la pool lo reutilize mas tarde
        poolDict[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}