using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public Camera cam;

    public Transform bulletPoint;
    public GameObject bulletPrefab;

    public float bulletForce;

    private Vector2 mousePos;
    void Start()
    {
        
    }

    void Update()
    {

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookDir = mousePos - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        //transform.rotation = angle;

        //Shooting
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }


    public void Shoot()
    {
        GameObject newBullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
        Rigidbody2D rB = newBullet.GetComponent<Rigidbody2D>();
        rB.AddForce(bulletPoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
