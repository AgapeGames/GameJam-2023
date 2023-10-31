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
    public bool isSuicide;

    public float suicideTime;
    public float suicideTimeCounter;

    public float rangeSuicide;
    public int damageBomb;

    public Color targetColor; // target color is red

    public GameObject effectBomb;
    public GameObject effectDie;

    public Item itemSpawn;
    public int minSpawn, maxSpawn;

    public AudioClip clipExplode;
    public AudioClip clipDie;
    IEnumerator ChangeColor()
    {
        Color initialColor = spriteCharacter.color; // get the initial color
        float timer = 0.0f;

        while (timer < suicideTime)
        {
            timer += Time.deltaTime;
            spriteCharacter.color = Color.Lerp(initialColor, targetColor, timer / suicideTime);
            yield return null; // wait one frame
        }

        if (Vector3.Distance(transform.position, targetPlayer.position) < rangeSuicide)
        {
            targetPlayer.GetComponent<PlayerControl>().Health(-damageBomb);
        }
        if (Vector3.Distance(transform.position, targetPlant.position) < rangeSuicide)
        {
            targetPlant.GetComponent<TreeControl>().Health(-damageBomb);
        }

        SoundManager.Instance.PlaySFX(clipExplode);

        GameObject obj = Instantiate(effectBomb, transform.position, transform.rotation);
        Destroy(obj, 2f);
        Destroy(gameObject);
    }
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
        if(isSuicide)
        {
            if(suicideTimeCounter > 0)
            {
                suicideTimeCounter -= Time.deltaTime;
            }
            else
            {
                isSuicide = false;
                StartCoroutine(ChangeColor());
            }
        }

        if (isStop) return;

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
            isSuicide = true;
        }
        else
        {
            isStop = false;
        }


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
            SoundManager.Instance.PlaySFX(clipDie);
            GameManager.Instance.EnemyDie(this);
            GameObject obj = Instantiate(effectDie, transform.position, transform.rotation);
            GameObject objItem = Instantiate(itemSpawn.gameObject, transform.position, Quaternion.identity);
            objItem.GetComponent<Item>().Spawn();
            objItem.GetComponent<Item>().SetRangeCount(4, 6);
            Destroy(obj, 2f);
            Destroy(gameObject);
        }
    }

    public void Suicide()
    {
        isSuicide = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Tree"))
        {
            isStop = true;
            isSuicide = true;
        }

        if (collision.gameObject.tag.Equals("Player"))
        {
            spriteCharacter.color = targetColor;

            if (Vector3.Distance(transform.position, targetPlayer.position) < rangeSuicide)
            {
                targetPlayer.GetComponent<PlayerControl>().Health(-damageBomb);
            }
            if (Vector3.Distance(transform.position, targetPlant.position) < rangeSuicide)
            {
                targetPlant.GetComponent<TreeControl>().Health(-damageBomb);
            }

            SoundManager.Instance.PlaySFX(clipExplode);

            GameObject obj = Instantiate(effectBomb, transform.position, transform.rotation);
            Destroy(obj, 2f);
            Destroy(gameObject);
        }
    }
}
