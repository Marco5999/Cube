using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 20f;
    public float fireRate = 0.2f; // Delay between shots to avoid instant shooting

    private float nextFireTime = 0f; // Keeps track of when the next shot can be fired

    void Update()
    {
        // Check if the fire button (mouse button or a key) is being held down
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime) 
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // Set the time for the next shot
        }
    }

    void Shoot()
    {
        // Instantiate a bullet and add force to it
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        }
    }
}
