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


    public TextMeshProUGUI textHealth;
    public Slider sliderHealth;

    public float timerSpawnLeafMin, timerSpawnLeafMax;
    private float timerSpawnLeafCounter;
    public GameObject prefabLeaf;

    public float timerSpawnAppleMin, timerSpawnAppleMax;
    private float timerSpawnAppleCounter;
    public GameObject prefabApple;

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
        for (int i = 0; i < 4; i++)
        {
            if (i == level - 1)
            {
                listSkin[i].SetActive(true);
            }
            else
            {
                listSkin[i].SetActive(false);
            }
        }
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

        if(this.health <= 0)
        {
            GameManager.Instance.GameLose();
        }
    }

    public void ProcessDrop()
    {

        if (level < 3) return;
        timerSpawnLeafCounter -= Time.deltaTime;
        timerSpawnAppleCounter -= Time.deltaTime;

        if(timerSpawnLeafCounter <= 0)
        {
            //Drop item
            Vector3 newPos = new Vector3(posSpawnItem.position.x + Random.Range(-3f,3f), posSpawnItem.position.y + Random.Range(-1f, 1f), 1);
            GameObject objLeaf = Instantiate(prefabLeaf, newPos, Quaternion.identity);
            objLeaf.GetComponent<Item>().Spawn();
            timerSpawnLeafCounter = Random.Range(timerSpawnLeafMin, timerSpawnLeafMax);
        }
        if (timerSpawnAppleCounter <= 0)
        {
            //Drop item
            Vector3 newPos = new Vector3(posSpawnItem.position.x + Random.Range(-3f, 3f), posSpawnItem.position.y + Random.Range(-1f, 1f), 1);
            GameObject objApple = Instantiate(prefabApple, newPos, Quaternion.identity);
            objApple.GetComponent<Item>().Spawn();
            timerSpawnAppleCounter = Random.Range(timerSpawnAppleMin, timerSpawnAppleMax);
        }
    }
}
