using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    public bool isActive;
    [Header("Profile")]
    public PlayerAnim playerAnim;

    public float health;

    public float moveSpeed;
    public float moveSpeedDash;

    public float timerDash;
    public float timerDashCounter;

    public float timerDashDelay;
    public float timerDashCounterDelay;
    public Image imageCDDash;

    public bool isDash;

    [Header("Component")]
    public Rigidbody2D rigidBody;
    public SpriteRenderer spriteCharacter;

    private Vector2 movement;


    public Item currentItem;

    public TextMeshProUGUI textHealth;
    public Slider sliderHealth;

    [Header("Utils")]
    public float maxRangeGreen;
    public float[] listRangeGreen;
    public bool isInZone;


    [Header("Sound")]
    public AudioClip clipDash;
    void Start()
    {
        timerDashCounterDelay = timerDashDelay;
        imageCDDash.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        if (movement.x < 0)
        {
            spriteCharacter.flipX = true;
        }
        else if (movement.x > 0)
        {
            spriteCharacter.flipX = false;
        }

        if(movement.y > 0)
        {
            playerAnim.SetSpriteUp();
        }else if (movement.y < 0)
        {
            playerAnim.SetSpriteDown();
        }
        else
        {
            playerAnim.SetSpriteSide();
        }

        if (timerDashCounterDelay >= timerDashDelay)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                SoundManager.Instance.PlaySFX(clipDash);
                isDash = true;
                timerDashCounter = timerDash;
                timerDashCounterDelay = 0;
            }
            imageCDDash.fillAmount = 1;
        }
        else
        {
            timerDashCounterDelay += Time.deltaTime;
            imageCDDash.fillAmount = timerDashCounterDelay / timerDashDelay;
        }


        if (isDash)
        {
            if (timerDashCounter > 0)
            {
                timerDashCounter -= Time.deltaTime;
            }
            else
            {
                isDash = false;
            }
        }

        //Get Item
        if(currentItem != null)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                currentItem.GetItem();
                currentItem = null;
            }
        }
        CheckDistanceTree();
        //Min Health
        if (!isInZone)
        {
            health -= Time.deltaTime;

            if (health <= 0)
            {
                GameManager.Instance.GameLose();
            }
        }
        textHealth.text = $"{(int)this.health}/{100}";
        sliderHealth.value = health;
    }

    private void FixedUpdate()
    {   
        if (!isActive) return;
        if (isDash)
        {
            rigidBody.MovePosition(rigidBody.position + movement * moveSpeedDash * Time.fixedDeltaTime);
        }
        else
        {
            rigidBody.MovePosition(rigidBody.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    public void Health(int health)
    {
        if (!isActive) return;
        this.health += health;

        if (this.health <= 0)
        {
            GameManager.Instance.GameLose();
        }
    }
    public void CheckDistanceTree()
    {
        maxRangeGreen = listRangeGreen[TreeControl.Instance.level - 1];

        if (Vector3.Distance(transform.position, GameManager.Instance.positionTree.position) > maxRangeGreen)
        {
            OutZone();
        }
        else
        {
            InZone();
        }
    }
    public void OutZone()
    {
        isInZone = false;
    }
    public void InZone()
    {
        isInZone = true;
    }
}
