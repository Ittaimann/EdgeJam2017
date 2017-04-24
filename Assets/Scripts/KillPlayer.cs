using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour {

    private void OnCollisionStay2D(Collision2D collision)
    {
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            playerHealth.Kill();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if(playerHealth != null )
                playerHealth.Kill();
        }
    }
}
