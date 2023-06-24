using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spanwer : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject Prefab;
        [Range(0f,1f)]
        public float spawnChance;
    }

    [SerializeField] private SpawnableObject[] objects;

    [SerializeField] private float minSpawnRate = 1f;
    [SerializeField] private float maxSpawnSRate = 2f;

    private void OnEnable()
    {
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnSRate));
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
 
    private void Spawn()
    {
        float spawnChance = Random.value;

        foreach (var obj in objects)
        {
            if (spawnChance < obj.spawnChance)
            {
              GameObject obstacle =  Instantiate(obj.Prefab);
              obstacle.transform.position += transform.position;
              break;
            }

            spawnChance -= obj.spawnChance;
        }
        
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnSRate));
    }
}
