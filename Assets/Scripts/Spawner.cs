using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    public float minSpawnDelay;
    public float maxSpwanDelay;

    [Header("References")]

    public GameObject[] gameObjects;
    void OnEnable()
    {
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpwanDelay));
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    // Update is called once per frame
    void Spawn()
    {
        GameObject randomObject = gameObjects[Random.Range(0, gameObjects.Length)];
        Instantiate(randomObject, transform.position, Quaternion.identity);
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpwanDelay));
    }
}
