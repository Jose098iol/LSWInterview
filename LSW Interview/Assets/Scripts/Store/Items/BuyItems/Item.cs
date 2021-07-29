using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Item : MonoBehaviour
{
    private string nameItem;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI priceItem;
    [SerializeField] private TextMeshProUGUI amountItem;
    [SerializeField] private TextMeshProUGUI notEnoughCoins;

    private int currentGold;
    private int currentAmount;
    private int currentPrice;

    private PlayerItems PlayerItems;

    private void Start()
    {
        PlayerItems = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerItems>();
    }

    private void Update()
    {
        currentGold = PlayerItems.Gold;
    }

    public void CreateItem(TemplateItem templateItem)
    {
        nameItem = templateItem.nameItem;
        itemIcon.sprite = templateItem.iconItem.sprite;
        currentPrice = templateItem.priceItem;
        priceItem.text = currentPrice.ToString();
        currentAmount = templateItem.amountItem;
        amountItem.text = currentAmount.ToString();
    }

    public void MoreAmount()
    {
        if(currentAmount < 100)
        {
            currentAmount += 10;
            currentPrice += 100;
            amountItem.text = currentAmount.ToString();
            priceItem.text = currentPrice.ToString();
        }
    }

    public void LessAmount()
    {
        if (currentAmount > 10)
        {
            currentAmount -= 10;
            currentPrice -= 100;
            amountItem.text = currentAmount.ToString();
            priceItem.text = currentPrice.ToString();
        }
    }

    public void BuyingItem()
    {
        CanBuyItem();
    }

    private void CanBuyItem()
    {
       if(currentGold < currentPrice)
        {
            StartCoroutine("notEnoughCoinsOff");
        }
        else
        {
            DetectCurrentItem();
        }
    }

    private void DetectCurrentItem()
    {
        if (nameItem == "Wood") { PlayerItems.MoreWood(currentAmount, currentPrice); }
        if (nameItem == "Crystal") { PlayerItems.MoreCrystal(currentAmount, currentPrice); }
        if (nameItem == "Rock") { PlayerItems.MoreRock(currentAmount, currentPrice); }
    }

    private IEnumerator notEnoughCoinsOff()
    {
        notEnoughCoins.enabled = true;
        yield return new WaitForSecondsRealtime(1f);
        notEnoughCoins.enabled = false;
    }
}
