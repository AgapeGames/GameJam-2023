using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PanelSkill : MonoBehaviour
{
    public PlayerControl player;
    public TextMeshProUGUI textAttack;
    public TextMeshProUGUI textDash;
    public TextMeshProUGUI textUpAttack;
    public TextMeshProUGUI textUpDash;

    public int[] priceAttack;
    public int[] priceDash;

    public Button buttonAttack;
    public Button buttonDash;

    void Start()
    {
        
    }

    public void UpgradeAttack()
    {
        if(ResourceManager.Instance.apple >= priceAttack[player.levelAttack])
        {
            ResourceManager.Instance.Apple(-priceAttack[player.levelAttack]);
            player.UpgradeShoot();
        }
    }
    public void UpgradeDash()
    {
        if (ResourceManager.Instance.apple >= priceDash[player.levelDash])
        {
            ResourceManager.Instance.Apple(-priceDash[player.levelDash]);
            player.UpgradeDash();
        }
    }

    public void RefreshText()
    {
        textAttack.text = $"Upgrade Speed Attack\n{player.levelAttack + 1}/5";
        textDash.text = $"Upgrade Dash Cooldown\n{player.levelDash + 1}/5";

        if (player.levelAttack >= 4)
        {
            textUpAttack.text = $"Max";
            buttonAttack.interactable = false;
        }
        else
            textUpAttack.text = $"{priceAttack[player.levelAttack]} Apple";

        if (player.levelDash >= 4)
        {
            textUpDash.text = $"Max";
            buttonDash.interactable = false;
        }
        else
            textUpDash.text = $"{priceDash[player.levelDash]} Apple";

    }
}
