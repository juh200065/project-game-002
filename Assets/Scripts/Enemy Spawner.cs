using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public List<SpawnableObject> spawnableObjects;

    private float startTime;

    void OnEnable()
    {
        startTime = Time.time;
        Invoke("Spawn", Random.Range(1f, 3f)); // 기본 딜레이
    }

    void Spawn()
    {
        List<SpawnableObject> valid = new();

        foreach (var obj in spawnableObjects)
        {
            if (Time.time - startTime >= obj.appearAfterSeconds && Random.value <= obj.spawnChance)
                valid.Add(obj);
        }

        if (valid.Count > 0)
        {
            var selected = valid[Random.Range(0, valid.Count)];
            Instantiate(selected.prefab, transform.position, Quaternion.identity);
  
            Invoke("Spawn", Random.Range(selected.minSpawnDelay, selected.maxSpawnDelay));
        }
    }
}