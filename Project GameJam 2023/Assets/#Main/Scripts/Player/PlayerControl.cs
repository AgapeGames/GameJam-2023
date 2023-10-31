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
    public PlayerShoot playerShoot;

    public float health;

    public float moveSpeed;
    public float moveSpeedDash;

    public float timerDash;
    public float timerDashCounter;

    public float timerDashDelay;
    public float timerDashCounterDelay;
    public Image imageCDDash;

    public int levelAttack = 1;
    public float effectAttack;
    public int levelDash;
    public float effectDash = 1;

    public bool isDash;

    [Header("Component")]
    public Rigidbody2D rigidBody;
    public SpriteRenderer spriteCharacter;

    private Vector2 movement;


    public Item currentItem;

    public TextMeshProUGUI textHealth;
    public Slider sliderHealth;

    [Header("Utils")]
    public float rangeRegen;
    public float maxRangeGreen;
    public float[] listRangeGreen;
    public bool isOutZone;

    public TextInfo textInfo;
    public Transform posSpawnText;
    public Transform objectCanvas;


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

        if (Input.GetKeyDown(KeyCode.S))
            playerAnim.AnimDown(true);
        else if (Input.GetKeyUp(KeyCode.S))
            playerAnim.AnimDown(false);

        if (Input.GetKeyDown(KeyCode.W))
            playerAnim.AnimUp(true);
        else if (Input.GetKeyUp(KeyCode.W))
            playerAnim.AnimUp(false);

        if (Input.GetKeyDown(KeyCode.A))
            playerAnim.AnimLeft(true);
        else if (Input.GetKeyUp(KeyCode.A))
            playerAnim.AnimLeft(false);

        if (Input.GetKeyDown(KeyCode.D))
            playerAnim.AnimRight(true);
        else if (Input.GetKeyUp(KeyCode.D))
            playerAnim.AnimRight(false);


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
        CalculateZone();


        textHealth.text = $"{(int)this.health}/{100}";
        sliderHealth.value = health;
    }

    public void SetCurrentItem(Item currentItem)
    {
        if(this.currentItem != null)
            this.currentItem.interactionPopup.SetActive(false);

        this.currentItem = currentItem;
        this.currentItem.interactionPopup.SetActive(true);

    }
    public void RemoveCurrentItem(Item currentItem)
    {
        if (this.currentItem == currentItem)
        {
            currentItem.interactionPopup.SetActive(false);
            this.currentItem = null;
        }
    }

    public float counterOutZone;

    private void CalculateZone()
    {
        //Min Health
        if (isOutZone)
        {
            counterOutZone -= Time.deltaTime;

            if (counterOutZone < 0)
            {
                counterOutZone = 1;

                if (health > 0)
                    Health(-1);
            }
        }
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


        GameObject newTextInfo = Instantiate(textInfo.gameObject, posSpawnText.position, posSpawnText.rotation, objectCanvas.transform);
        TextInfo newInfo = newTextInfo.GetComponent<TextInfo>();
            
        if (health > 0)
            newInfo.ShowText("+" + ((int)health).ToString(), 1);
        if (health < 0)
            newInfo.ShowText(((int)health).ToString());

        this.health += health;
        if (this.health >= 100) this.health = 100;

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
            isOutZone = true;
        }
        else
        {
            isOutZone = false;
        }
    }

    public void UpgradeShoot()
    {
        levelAttack++;
        playerShoot.timerShoot = 1 - (levelAttack * effectAttack);
        CanvasManager.Instance.panelSkill.RefreshText();
    }
    public void UpgradeDash()
    {
        levelDash++;
        timerDashDelay = 2.5f - (levelDash * effectDash);
        CanvasManager.Instance.panelSkill.RefreshText();
    }
}
