using Enemies;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    //[SerializeField] private GameObject characterPrefab;
    [SerializeField] private int spawnsPerPeriod = 10;
    [SerializeField] private float frequency = 30;
    [SerializeField] private float period = 0;
    private ObjectPool objectPooler;

    private void OnEnable()
    {
        if (frequency > 0) period = 1 / frequency;
    }

    private IEnumerator Start()
    {
        //el codigo entero depende de este segundo :)
        yield return new WaitForSeconds(1f);

        objectPooler = ObjectPool.Instance; //llamamos la instancia misma pq si no me tiraba error

        //cambie todo el codigo porque si no me tiraba error de navMesh

        while (true)
        {
            for (int i = 0; i < spawnsPerPeriod; i++)
            {
                //llamamos a la pool en vez de a la instanciacion
                GameObject enemyObj = objectPooler.SpawnFromPool("Enemy", transform.position, Quaternion.identity);
                //Instantiate(characterPrefab, transform.position, transform.rotation);

                //Busca al enemigo para spawnearlo
                Enemy enemy = enemyObj.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.OnSpawnFromPool();
                }

            }

            yield return new WaitForSeconds(period);
        }
    }
}
