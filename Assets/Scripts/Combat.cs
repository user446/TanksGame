using UnityEngine;
using System;

//class for close combat
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

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
