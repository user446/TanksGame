using UnityEngine;
using System;

/// <summary>
/// Class for close combat
/// Calls for Attack() are allowed each 1/attackCooldown second
/// </summary>
public class Combat : MonoBehaviour
{
    public float attackDamage;
    public float attackRange;
    public float attackSpeed;
    private float attackCooldown;
    public LayerMask attackLayer;

    // Update is called once per frame
    void Update()
    {
        if(attackCooldown >= 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Checks if any prefab on attackLayer is inside circle of attackRange and applies damage based on armor of these prefabs
    /// </summary>
    public void Attack()
    {
        if(attackCooldown < 0)
        {
            attackCooldown = 1f/attackSpeed;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange, attackLayer);
            foreach(var c in colliders)
            {
                var c_health = c.GetComponent<Health>();
                var c_armor = c.GetComponent<Armor>();
                if(c_health != null)
                {
                    if(c_armor != null)
                    {
                        c_health.DoDelta(-attackDamage * (1 - c_armor.absorbPercentage));
                        // if attack aim is a monster then count damange into a Score UI
                        if(c.tag == "Monster" && c.GetComponent<MonsterBase>() != null)
                            c.GetComponent<MonsterBase>().onTakingDamage(attackDamage * (1 - c_armor.absorbPercentage));
                    }
                    else
                    {
                        c_health.DoDelta(-attackDamage);
                        if(c.tag == "Monster" && c.GetComponent<MonsterBase>() != null)
                            c.GetComponent<MonsterBase>().onTakingDamage(attackDamage);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Makes easier to ajust attack range in the Inspector
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
