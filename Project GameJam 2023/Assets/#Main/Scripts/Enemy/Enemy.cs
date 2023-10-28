using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float moveSpeed;

    public SpriteRenderer spriteCharacter;

    public Transform target;

    public Transform targetPlant;
    public Transform targetPlayer;

    public float rangeDetectionPlayer;
    public float rangeStop;


    public bool isStop;
    void Start()
    {
        
    }

    public void Init()
    {
        targetPlant = GameManager.Instance.positionTree;
        targetPlayer = GameManager.Instance.positionPlayer;
        target = targetPlant;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, targetPlayer.position) < rangeDetectionPlayer)
        {
            target = targetPlayer;
        }
        else
        {
            target = targetPlant;
        }

        if (Vector3.Distance(transform.position, target.position) < rangeStop)
        {
            isStop = true;
        }
        else
        {
            isStop = false;
        }

        if (isStop) return;

        Vector3 direction = target.position - transform.position;

        if (target.position.x < transform.position.x)
        {
            spriteCharacter.flipX = true;
        }
        else if (target.position.x > transform.position.x)
        {
            spriteCharacter.flipX = false;
        }

        direction.Normalize();



        Vector3 newPosition = transform.position + direction * moveSpeed * Time.deltaTime;

        transform.position = newPosition;
    }

    public void GetDamage()
    {
        health--;

        if(health <= 0)
        {
            GameManager.Instance.EnemyDie(this);
            Destroy(gameObject);
        }
    }
}
