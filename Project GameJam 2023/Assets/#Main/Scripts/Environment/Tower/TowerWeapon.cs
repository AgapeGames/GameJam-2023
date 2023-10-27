using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerWeapon : MonoBehaviour
{
    public float rangeDetection;

    public Transform target;

    public Transform bulletPoint;
    public GameObject bulletPrefab;

    public float bulletSpeed;

    void Start()
    {
    }

    void Update()
    {



        Vector3 direction = target.position - transform.position;
        direction.z = 0;
        transform.up = direction;

        //Shooting
        if (Input.GetButtonDown("Fire2"))
        {
            Shoot();
        }
    }


    public void Shoot()
    {

        // Buat instance dari bullet prefab
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Menghitung arah dari objek ke mouse
        Vector3 direction = (target.position - transform.position).normalized;

        // Menambahkan kecepatan kepada bullet agar bergerak ke arah mouse
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}
