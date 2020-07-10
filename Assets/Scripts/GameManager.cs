using UnityEngine;

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

    void MonsterHolder()
    {
        monsters = FindObjectsOfType<MonsterBase>();
        if(monsters.Length >= monsterCountMax)
        {
            monsterSpawnEnabled = false;
        }
        else
            monsterSpawnEnabled = true;
    }

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
