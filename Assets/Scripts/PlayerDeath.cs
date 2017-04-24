using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class PlayerDeath : MonoBehaviour
{
    public GameObject deathParticle;
    Health health;
    SpriteRenderer sprite;
    public AudioClip deathSound;
    
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
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        if (deathSound)
        {
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
        }
        
        if(GameManager.Instance.CurrentCamera().GetComponent<ScreenShake>())
            GameManager.Instance.CurrentCamera().GetComponent<ScreenShake>().screenShake(0.01f,1.5f);
        gameObject.SetActive(false);
        health.onDeath -= Health_onDeath;
        GameManager.Instance.PlayerDied();
    }
}
