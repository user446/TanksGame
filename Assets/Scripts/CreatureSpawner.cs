using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    private float spawnCooldown;
    public float spawnSpeed;
    public List<GameObject> creatureList;

    void Update()
    {
        spawnCooldown -= Time.deltaTime;
    }

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
