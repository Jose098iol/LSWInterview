using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SellItem : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI GoldToWin;
    [SerializeField] private TextMeshProUGUI AmountToSell;

    private string nameItem;
    private int currentItem;
    private int goldToWin;
    private int amountToSell;

    private PlayerItems PlayerItems;

    public void CreateItem(TemplateItem templateItem)
    {
        nameItem = templateItem.nameItem;
        itemIcon.sprite = templateItem.iconItem.sprite;
        goldToWin = 0;
        amountToSell = 0;
        GoldToWin.text = amountToSell.ToString();
        AmountToSell.text = goldToWin.ToString();

        PlayerItems = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerItems>();
        DetectCurrentAmountItem();
    }
    private void OnEnable()
    {
        DetectCurrentAmountItem();
    }
    private void DetectCurrentAmountItem()
    {
        if (nameItem == "Wood") { currentItem = PlayerItems.amountWood; }
        if (nameItem == "Crystal") { currentItem = PlayerItems.amountCrystal; }
        if (nameItem == "Rock") { currentItem = PlayerItems.amountRock; }
    }
    public void MoreAmount()
    {
        if (amountToSell < currentItem)
        {
            amountToSell += 10;
            goldToWin += 100;
            AmountToSell.text = amountToSell.ToString();
            GoldToWin.text = goldToWin.ToString();
        }
    }

    public void LessAmount()
    {
        if (amountToSell > 0)
        {
            amountToSell -= 10;
            goldToWin -= 100;
            AmountToSell.text = amountToSell.ToString();
            GoldToWin.text = goldToWin.ToString();
        }
    }

    public void sellingItem()
    {
        CanSellItem();
    }

    private void CanSellItem()
    {
        if (amountToSell <= currentItem)
        {
            SellCurrentItem();
            restartSell();
        }
    }
    private void restartSell()
    {
        amountToSell = 0;
        goldToWin = 0;
        AmountToSell.text = amountToSell.ToString();
        GoldToWin.text = goldToWin.ToString();

        DetectCurrentAmountItem();
    }
    private void SellCurrentItem()
    {
        if (nameItem == "Wood") { PlayerItems.LessWood(amountToSell, goldToWin); }
        if (nameItem == "Crystal") { PlayerItems.LessCrystal(amountToSell, goldToWin); }
        if (nameItem == "Rock") { PlayerItems.LessRock(amountToSell, goldToWin); }
    }

}
