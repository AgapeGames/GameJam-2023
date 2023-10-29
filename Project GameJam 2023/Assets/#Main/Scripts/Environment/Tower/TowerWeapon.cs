using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerWeapon : MonoBehaviour
{
    public TowerControl control;
    public float rangeDetection;

    public float timeReload;
    private float timeReloadCounter;

    public GameObject target;

    public Transform bulletPoint;
    public GameObject bulletPrefab;

    public float bulletSpeed;
    private List<GameObject> enemiesInRange = new List<GameObject>();

    public Transform posShooting;

    public Sprite spriteTowerDown;
    public Sprite spriteTowerUp;

    public SpriteRenderer srTower;
    public SpriteRenderer spriteShooting;
    void Start()
    {
    }

    void Update()
    {
        if (!control.isActive) return;

        float minDistance = float.MaxValue;  // Set ke nilai maksimum pada awalnya

        // Iterasi melalui daftar musuh
        for (int i = 0; i < enemiesInRange.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, enemiesInRange[i].transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                target = enemiesInRange[i];
            }
        }

        if (target == null) return;

        if(target.transform.position.y > transform.position.y)
        {
            srTower.sprite = spriteTowerUp;
            spriteShooting.sortingOrder = -1;
        }
        else if (target.transform.position.y < transform.position.y)
        {
            spriteShooting.sortingOrder = 2;
            srTower.sprite = spriteTowerDown;
        }

        Vector3 direction = target.transform.position - transform.position;
        direction.z = 0;
        transform.up = direction;

        //Shooting
        if (timeReloadCounter <= 0)
        {
            timeReloadCounter = timeReload;
            Shoot();
        }
        else
        {
            timeReloadCounter -= Time.deltaTime;
        }
    }


    public void Shoot()
    {

        // Buat instance dari bullet prefab
        GameObject bullet = Instantiate(bulletPrefab, posShooting.position, posShooting.rotation);

        // Menghitung arah dari objek ke mouse
        Vector3 direction = (target.transform.position - transform.position).normalized;

        bullet.transform.up = direction;
        // Menambahkan kecepatan kepada bullet agar bergerak ke arah mouse
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        target = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Enemy")  // Ganti "Enemy" dengan tag yang digunakan pada musuh Anda
        {
            enemiesInRange.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")  // Ganti "Enemy" dengan tag yang digunakan pada musuh Anda
        {
            enemiesInRange.Remove(collision.gameObject);
        }
    }


}
