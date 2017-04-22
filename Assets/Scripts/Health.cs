using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public delegate void OnDamage(float amount);
    public delegate void HealthChanged();
    public delegate void OnDeath();

    public event OnDamage onDamage = delegate { };
    public event HealthChanged onHealthChanged = delegate { };
    public event OnDeath onDeath = delegate { };

    [SerializeField]
    protected float health = 100f;
    [SerializeField]
    protected float maxHealth = 100f;

    public float HealthValue { get { return health; } }
    public float MaxHealthValue { get { return maxHealth; } }
    public float healthPercent { get { return health / maxHealth; } }

    public void Awake()
    {
        maxHealth = health;
    }

    public float SetHealth(float value)
    {
        float priorHealth = health;
        health = value;
        if(health > maxHealth)
            health = maxHealth;

        onHealthChanged();

        if (health <= 0)
            onDeath();

        return priorHealth - health;
    }

    public virtual float Damage(float amount)
    {
        onDamage(amount);
        return SetHealth(health - amount);
    }

    public void Kill()
    {
        SetHealth(0);
    }

}
