using UnityEngine;

public class MonsterBase : MonoBehaviour
{
    public Health health;
    public Armor armor;
    public Combat combat;
    private MonsterBrain brain;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            var pr = collision.collider.GetComponent<Projectile>();
            if(pr != null)
            {
                Debug.Log("Hit by projectile");
                if(health != null)
                {
                    if(armor != null)
                    {
                        health.DoDelta(-pr.damage * (1 - armor.absorbPercentage));
                    }
                    else
                        health.DoDelta(-pr.damage);
                }
            }
        }
    }

    void Start()
    {
        health = GetComponent<Health>();
        armor = GetComponent<Armor>();
        combat = GetComponent<Combat>();
        brain = GetComponent<MonsterBrain>();

        brain.SetTarget(GameManager.GetPlayer().transform);
        health.onDeath += Death;
    }

    void Update()
    {
        
    }

    void Death()
    {
        Debug.Log(gameObject.name + " is dead now");
        Destroy(gameObject);
    }
}
