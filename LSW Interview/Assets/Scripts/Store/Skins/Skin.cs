using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skin : MonoBehaviour
{
    private string nameSkin;
    [SerializeField] private Image skinIcon;
    [SerializeField] private TextMeshProUGUI priceSkin;
    [SerializeField] private TextMeshProUGUI notEnoughCoins;
    [SerializeField] private GameObject BuyButton;
    [SerializeField] private GameObject UseButton;
    [SerializeField] private GameObject Using;

    private int currentGold;
    private int skinPrice;

    private GameObject PlayerOb;
    private PlayerItems PlayerItems;
    private ReceiveCurrentSkin receiveCurrentSkin;

    private SkinsPanel SkinsPanel;

    private TemplateSkin CurrentTemplate;

    private string currentSkin;

    private void Start()
    {
        
        PlayerOb = GameObject.FindGameObjectWithTag("Player");
        PlayerItems = PlayerOb.GetComponent<PlayerItems>();
        receiveCurrentSkin = PlayerOb.GetComponent<ReceiveCurrentSkin>();
    }

    private void Update()
    {
        currentGold = PlayerItems.Gold;
    }

    public void CreateItem(TemplateSkin templateSkin)
    {
        SkinsPanel = GameObject.FindGameObjectWithTag("SkinPanel").GetComponent<SkinsPanel>();
        CurrentTemplate = templateSkin;
        nameSkin = CurrentTemplate.nameSkin;
        skinIcon.sprite = CurrentTemplate.iconSkin.sprite;
        skinPrice = CurrentTemplate.priceSkin;
        priceSkin.text = skinPrice.ToString();

        if (nameSkin == "Man") { priceSkin.text = "Free"; PlayerPrefs.SetInt(nameSkin, 1); }

        initialState();
    }

    public void initialState()
    {
        currentSkin = PlayerPrefs.GetString("CurrentSkin", "Man");

        if (nameSkin != currentSkin)
        {
            int isBuyed = PlayerPrefs.GetInt(nameSkin, 0);

            if (isBuyed == 1)
            {
                showBuyedUI();

                if (nameSkin != "Man") { priceSkin.text = "Buyed"; }
            }
            else if (isBuyed == 0)
            {
                buySkin();
            }
        }
        else
        {
            UsedSkin();
        }
    }
    private void buySkin()
    {
        UseButton.SetActive(false);
        Using.SetActive(false);
        BuyButton.SetActive(true);
    }
    private void UsedSkin()
    {
        BuyButton.SetActive(false);
        UseButton.SetActive(false);
        Using.SetActive(true);
    }
    private void showBuyedUI()
    {
        PlayerPrefs.SetInt(nameSkin, 1);
        
        BuyButton.SetActive(false);
        Using.SetActive(false);
        UseButton.SetActive(true);
    }

    public void EquipingSkin()
    {
        DetectCurrentSkin();
    }
    private void DetectCurrentSkin()
    {
        receiveCurrentSkin.updateSkin(nameSkin);

        SkinsPanel.RestartButtons();
    }

    public void BuyingSkin()
    {
        CanBuySkin();
    }
    private void CanBuySkin()
    {
        if (currentGold < skinPrice)
        {
            notEnoughCoins.enabled = true;
            StartCoroutine("notEnoughCoinsOff");
        }
        else
        {
            showBuyedUI();
            PlayerItems.buySkin(skinPrice);
            SkinsPanel.RestartButtons();
        }
    }

    private IEnumerator notEnoughCoinsOff()
    {
        yield return new WaitForSecondsRealtime(1f);
        notEnoughCoins.enabled = false;
    }

}
