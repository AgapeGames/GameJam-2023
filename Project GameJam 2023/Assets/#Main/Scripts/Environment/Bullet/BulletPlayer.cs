using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    public float timeDestroy = .75f;
    public GameObject effectDestroy;

    void Start()
    {
        Destroy(gameObject, timeDestroy);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.Equals("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().GetDamage();
        }


        Destroy(gameObject);
    }
}
