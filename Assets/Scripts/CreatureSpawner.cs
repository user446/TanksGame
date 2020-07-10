using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creature spawner class
/// Holds a list of creature prefabs and spawns random one each 1/spawnCooldown seconds
/// </summary>
public class CreatureSpawner : MonoBehaviour
{
    private float spawnCooldown;
    public float spawnSpeed;
    public List<GameObject> creatureList;

    void Update()
    {
        spawnCooldown -= Time.deltaTime;
    }

    /// <summary>
    /// Spawns a random creature once spawnCooldown has reached 0
    /// </summary>
    public void SpawnNewCreature()
    {
        if(spawnCooldown < 0)
        {
            if(creatureList.Count > 0)
                Instantiate(creatureList[Random.Range(0, creatureList.Count)], transform.position, Quaternion.identity);
            spawnCooldown = 1f/spawnSpeed;
        }
    }
}
