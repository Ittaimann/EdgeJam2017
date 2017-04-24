using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile {
    public override void OnTriggerStay2D(Collider2D other)
    {
        DestroySelf();
    }

    public override void DestroySelf()
    {
        Destroy(gameObject);
	}
}
