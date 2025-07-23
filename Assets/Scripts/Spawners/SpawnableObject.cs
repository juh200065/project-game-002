using UnityEngine; 

[CreateAssetMenu(fileName = "SpawnableObject", menuName = "Spawner/SpawnableObject")]
public class SpawnableObject : ScriptableObject
{
    public GameObject prefab;
    public float minSpawnDelay = 1f;
    public float maxSpawnDelay = 3f;
    public float spawnChance = 1f; 
    public float appearAfterSeconds = 0f; 
}