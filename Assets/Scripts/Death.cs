using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Death : MonoBehaviour {
    Health health;
    SpriteRenderer sprite;
    bool spriteDisable = false;

    void Start()
    {
        health = GetComponent<Health>();
        sprite = transform.root.GetComponentInChildren<SpriteRenderer>();
        health.onDeath += Health_onDeath;
        health.onDamage += Health_onDamage;
    }

    private void Health_onDamage(float amount)
    {
        Debug.Log("Health: " + health.healthPercent);
        StartCoroutine(_DamageFlash());
    }
    private IEnumerator _DamageFlash()
    {
        spriteDisable = true;
        sprite.enabled = false;
        yield return new WaitForSeconds(.05f);
        sprite.enabled = true;
    }

    private void Health_onDeath()
    {
        //screenshake
        Debug.Log(transform.name + " has died");
        gameObject.SetActive(false);
        //health.onDeath -= Health_onDeath;
        GameManager.Instance.PlayerDied();
    }
}
