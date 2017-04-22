using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

    public GameObject projectile;
    private Transform spawnPoint;
    private float timer = 0;
    public float shootInterval;
    public float startOffset;

    // Use this for initialization
    void Start () {
        spawnPoint = transform.GetChild(0);
        timer -= startOffset;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //Debug.Log(timer);
        if (timer > shootInterval)
        {
            Shoot();
            timer = 0;
        }
    }

    void Shoot()
    {
        GameObject shot = (GameObject)Instantiate(projectile, spawnPoint.position, transform.rotation);
        // Convert the angle of the player to the velocity of the bullet and shoot it forward
        Vector2 angle = Quaternion.AngleAxis(transform.rotation.eulerAngles.z, Vector3.forward) * Vector3.up;
        //Debug.Log("Angle" + angle + "speed" + shot.GetComponent<Projectile>().speed);
        shot.GetComponent<Rigidbody2D>().velocity = angle * shot.GetComponent<Projectile>().speed;
    }
}
