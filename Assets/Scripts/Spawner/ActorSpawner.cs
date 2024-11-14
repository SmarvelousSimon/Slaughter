namespace slaughter.de.Spawner
{
    // public class ActorSpawner : MonoBehaviour
    // {
    //     public GameObject spawningPrefab;
    //     public float spawnRadius = 5f;
    //     public float spawnFrequency = 1f;
    //     private float spawnTimer = 0f;
    //
    //     private void Update()
    //     {
    //         spawnTimer += Time.deltaTime;
    //
    //         if (spawnTimer >= spawnFrequency)
    //         {
    //             var randomPos = Random.insideUnitCircle * spawnRadius;
    //             // Instantiate(spawningPrefab, transform.position + new Vector3(randomPos.x, randomPos.y, 0f),
    //             //     Quaternion.identity);
    //
    //             GameObject actorToSpawn = ObjectPool.SharedInstance.GetPooledObject(); // das geht hier nicht
    //             if (actorToSpawn)
    //             {
    //                 // Transform pos = transform.GetChild(0).transform;
    //                 actorToSpawn.transform.position = randomPos;
    //                 actorToSpawn.SetActive(true);
    //             }
    //
    //             spawnTimer = 0f;
    //         }
    //     }
    // }
}