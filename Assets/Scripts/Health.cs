using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public bool isDead;
    public Action onDeath = delegate { };

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if(currentHealth <= 0 && !isDead)
        {
            isDead = true;
            onDeath();
        }
    }

    public void DoDelta(float delta)
    {
        currentHealth = Mathf.Clamp(currentHealth + delta, 0, maxHealth);
    }
}
