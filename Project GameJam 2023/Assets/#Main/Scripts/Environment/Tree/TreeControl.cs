using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TreeControl : MonoBehaviour
{
    public static TreeControl Instance;

    public int health;
    public int level;

    public int[] requirementWater;
    public int[] requirementFertilizer;

    public GameObject interactionPopup;

    public bool playerInside;


    public GameObject[] listSkin;
    public GameObject[] listSkinSprite;


    public TextMeshProUGUI textHealth;
    public Slider sliderHealth;

    public float timerSpawnLeafMin, timerSpawnLeafMax;
    private float timerSpawnLeafCounter;
    public GameObject prefabLeaf;
    public List<GameObject> listLeaf;
    public int maxSpawnLeaf;

    public float timerSpawnAppleMin, timerSpawnAppleMax;
    private float timerSpawnAppleCounter;
    public GameObject prefabApple;
    public List<GameObject> listApple;
    public int maxSpawnApple;

    public Transform posSpawnItem;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {

        timerSpawnLeafCounter = Random.Range(timerSpawnLeafMin, timerSpawnLeafMax);
        timerSpawnAppleCounter = Random.Range(timerSpawnAppleMin, timerSpawnAppleMax);
    }

    void Update()
    {
        ProcessDrop();

        textHealth.text = $"{(int)health}/{100}";
        sliderHealth.value = health;

        if (level < 5)
        {
            if (playerInside)
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    UpgradeTree();
                }
            }
        }

    }

    public void UpgradeTree()
    {
        if(ResourceManager.Instance.water >= GetNeedWater() 
            && ResourceManager.Instance.fertilizer >= GetNeedFertilizer())
        {
            ResourceManager.Instance.Water(-GetNeedWater());
            ResourceManager.Instance.Fertilizer(-GetNeedFertilizer());

            level++;
            Health(100);

            RefreshSize();
            RefreshPopUpTree();
            CanvasManager.Instance.RefreshResource();
        }


        if (level >= 5)
        {
            level = 5;
            GameManager.Instance.GameWin();
            return;
        }
    }

    public void RefreshSize()
    {
        Debug.Log("refresh " + level);
        for (int i = 0; i < level - 1; i++)
        {

            Debug.Log("aktif " + i);
            listSkin[i].SetActive(true);
            listSkinSprite[i].SetActive(false);

        }

        listSkinSprite[level - 1].SetActive(true);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            interactionPopup.SetActive(true);
            RefreshPopUpTree();
            playerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            interactionPopup.SetActive(false);
            playerInside = false;
        }
    }

    public TextMeshProUGUI textNeedWater;
    public TextMeshProUGUI textNeedFertilizer;
    public void RefreshPopUpTree()
    {
        textNeedWater.text = "" + GetNeedWater();
        textNeedFertilizer.text = "" + GetNeedFertilizer();
    }
    public int GetNeedWater()
    {
        if (level >= 5) return 999;
        return requirementWater[level - 1];
    }
    public int GetNeedFertilizer()
    {
        if (level >= 5) return 999;
        return requirementFertilizer[level - 1];
    }

    public void Health(int health)
    {
        this.health += health;

        if (this.health >= 100) this.health = 100;

        if (this.health <= 0)
        {
            GameManager.Instance.GameLose();
        }
    }

    public void ProcessDrop()
    {
        timerSpawnLeafCounter -= Time.deltaTime;
        timerSpawnAppleCounter -= Time.deltaTime;

        if(timerSpawnLeafCounter <= 0)
        {
            //Drop item
            if (listLeaf.Count <= maxSpawnLeaf)
            {

                Vector3 newPos = new Vector3(posSpawnItem.position.x + Random.Range(-3f, 3f), posSpawnItem.position.y + Random.Range(-1f, 1f), 1);
                GameObject objLeaf = Instantiate(prefabLeaf, newPos, Quaternion.identity);
                objLeaf.GetComponent<Item>().Spawn();
                timerSpawnLeafCounter = Random.Range(timerSpawnLeafMin, timerSpawnLeafMax);


                listLeaf.Add(objLeaf);
            }
        }
        if (timerSpawnAppleCounter <= 0)
        {
            if (listApple.Count <= maxSpawnApple)
            {
                //Drop item
                Vector3 newPos = new Vector3(posSpawnItem.position.x + Random.Range(-3f, 3f), posSpawnItem.position.y + Random.Range(-1f, 1f), 1);
                GameObject objApple = Instantiate(prefabApple, newPos, Quaternion.identity);
                objApple.GetComponent<Item>().Spawn();
                timerSpawnAppleCounter = Random.Range(timerSpawnAppleMin, timerSpawnAppleMax);

                listApple.Add(objApple);
            }
        }
    }

    public void RemoveLeaf(GameObject item)
    {
        listLeaf.Remove(item);
    }
    public void RemoveApple(GameObject item)
    {
        listApple.Remove(item);
    }
}
