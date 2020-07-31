using UnityEngine;
using System;

/// <summary>
/// Monster base class
/// </summary>
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Armor))]
[RequireComponent(typeof(Combat))]
[RequireComponent(typeof(MonsterBrain))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class MonsterBase : MonoBehaviour
{
    public Health health;
    public Armor armor;
    public Combat combat;
    private MonsterBrain brain;
    public Action<float> onTakingDamage;

    /// <summary>
    /// Each time monser is hit by a projectile we shoud count damage
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        //check if monster was damaged by a player
        if(collision.collider.tag == "Player")
        {
            //check if it is a projectile
            var pr = collision.collider.GetComponent<Projectile>();
            if(pr != null)
            {
                Debug.Log("Hit by projectile");
                if(health != null)
                {
                    if(armor != null)
                    {
                        health.DoDelta(-pr.damage * (1 - armor.absorbPercentage));
                        //send damage outside of a script
                        onTakingDamage(pr.damage * (1 - armor.absorbPercentage));
                    }
                    else
                    {
                        health.DoDelta(-pr.damage);
                        onTakingDamage(pr.damage);
                    }

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

    void Death()
    {
        Debug.Log(gameObject.name + " is dead now");
        Destroy(gameObject);
    }
}
