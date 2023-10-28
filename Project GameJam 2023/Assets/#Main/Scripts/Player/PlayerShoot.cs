using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{

    public Camera cam;

    public Transform bulletPoint;
    public GameObject bulletPrefab;

    public float bulletSpeed;

    private Vector3 mousePosition;



    public float timerShoot;
    public float timerShootCounter;
    public Image imageCDShoot;


    void Start()
    {
        timerShootCounter = timerShoot;
        imageCDShoot.fillAmount = 1;
    }

    void Update()
    {

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = mousePosition - transform.position;
        direction.z = 0;
        transform.up = direction;

        //Shooting
        if (timerShootCounter >= timerShoot)
        {
            imageCDShoot.fillAmount = 1;
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            timerShootCounter += Time.deltaTime;
            imageCDShoot.fillAmount = timerShootCounter / timerShoot;
        }
    }


    public void Shoot()
    {
        timerShootCounter = 0;
        // Dapatkan posisi mouse dalam ruang dunia
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // Buat instance dari bullet prefab
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);


        // Menghitung arah dari objek ke mouse
        Vector3 direction = (mousePosition - transform.position).normalized;

        bullet.transform.up = direction;

        // Menambahkan kecepatan kepada bullet agar bergerak ke arah mouse
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}
