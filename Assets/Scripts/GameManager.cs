using UnityEngine;
using System;

/// <summary>
/// Game Manager class
/// Manages the amount of monsters on a scene
/// Cleans all projectiles outcoming screen boundaries
/// Sends score updates to score UI
/// </summary>
public class GameManager : MonoBehaviour
{

    #region Singleton

    public static GameManager _instance;

    void Awake()
    {
        _instance = this;
        player = GameObject.Find("Player");
        sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        player.GetComponent<Health>().onDeath += GameOver;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        spawners = FindObjectsOfType<CreatureSpawner>();
    }

    #endregion

    public int monsterCountMax = 10;
    private bool monsterSpawnEnabled = true;
    private GameObject player;
    private Projectile[] projectiles;
    private CreatureSpawner[] spawners;
    private Vector2 screenBounds;
    private MonsterBase[] monsters;
    private SceneLoader sceneLoader;
    public ScoreUI scoreUI;

    void GameOver()
    {
        Debug.Log("GameOver");
        player.GetComponent<PlayerController>().enabled = false;
        sceneLoader.LoadEndGame();
    }

    public static GameObject GetPlayer()
    {
        return _instance.player;
    }

    void LateUpdate()
    {
        GarbageCollector();
        CallForNewCreatures();
        MonsterHolder();
    }

    void CountDamage(float dmg)
    {
        scoreUI.score += (int)dmg;
    }

    /// <summary>
    /// Call for new creatures on each Creature Spawner prefab if spawning is enabled
    /// </summary>
    void CallForNewCreatures()
    {
        foreach(var s in spawners)
        {
            if(monsterSpawnEnabled)
                s.SpawnNewCreature();
            else
            {
                break;
            }
        }
    }

    /// <summary>
    /// Counts the amount of monsters on a scene and
    /// disables spawning if their number have reached monsterCountMax
    /// applies CountDamage on onTakingDamage action to update Score UI
    /// </summary>
    void MonsterHolder()
    {
        monsters = FindObjectsOfType<MonsterBase>();
        if(monsters.Length >= monsterCountMax)
        {
            monsterSpawnEnabled = false;
        }
        else
            monsterSpawnEnabled = true;
        foreach(var m in monsters)
        {
            if(m.onTakingDamage != CountDamage)
                m.onTakingDamage = CountDamage;
        
        }
    }

    /// <summary>
    /// Destroys all projectiles outside screen boudaries since player can not reach anything outside
    /// </summary>
    void GarbageCollector()
    {
        projectiles = FindObjectsOfType<Projectile>();
        foreach(var pr in projectiles)
        {
            Vector3 viewPos = pr.transform.position;
            viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x, screenBounds.x * -1);
            viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y, screenBounds.y * -1);
            if(pr.transform.position.x > viewPos.x || pr.transform.position.y > viewPos.y
                || pr.transform.position.y < -viewPos.y || pr.transform.position.x < -viewPos.x)
            {
                Destroy(pr.gameObject);
            }
        }
    }
}
