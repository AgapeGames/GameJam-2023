using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public Camera cam;

    public Transform bulletPoint;
    public GameObject bulletPrefab;

    public float bulletSpeed;

    private Vector3 mousePosition;
    void Start()
    {
        
    }

    void Update()
    {

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = mousePosition - transform.position;
        direction.z = 0;
        transform.up = direction;

        //Shooting
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }


    public void Shoot()
    {
        // Dapatkan posisi mouse dalam ruang dunia
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // Buat instance dari bullet prefab
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Menghitung arah dari objek ke mouse
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Menambahkan kecepatan kepada bullet agar bergerak ke arah mouse
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}
