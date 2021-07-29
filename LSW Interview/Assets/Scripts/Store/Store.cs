using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public Transform pivotPlayerLeft;
    public Transform pivotPlayerRight;

    private Transform Player;
    [SerializeField] PlayerControl PlayerControl;

    [SerializeField] private GameObject StoreOb;
    [SerializeField] private GameObject StartStoreOb;
    [SerializeField] private GameObject SellPanel;
    [SerializeField] private GameObject BuyPanels;
    [SerializeField] private GameObject BuyItemsPanel;
    [SerializeField] private GameObject BuySkinsPanel;

    [SerializeField] private GameObject ClickToTalk;

    private bool Collecting;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    public void StorePressed()
    {
        Collecting = PlayerControl.Collecting;
        if (Collecting) { return; }

        PlayerControl.StorePressed = true;

        float xPosPlayer = Player.transform.position.x;
        if(xPosPlayer > transform.position.x)
        {
            PlayerControl.MovePlayerToStore(pivotPlayerRight.position.x, pivotPlayerRight.position.y);
            PlayerControl.ForLeft = false;
            PlayerControl.ForRight = true;
        }
        if (xPosPlayer < transform.position.x)
        {
            PlayerControl.MovePlayerToStore(pivotPlayerLeft.position.x, pivotPlayerLeft.position.y);
            PlayerControl.ForRight = false;
            PlayerControl.ForLeft = true;
        }
    }
    public void InStore()
    {
        StoreOb.SetActive(true);
        ClickToTalk.SetActive(false);
    }
    public void BuyPanelsOn()
    {
        StartStoreOb.SetActive(false);
        BuyItemPanelOn();
        BuyPanels.SetActive(true);
    }
    public void BuyItemPanelOn()
    {
        BuySkinsPanel.SetActive(false);
        BuyItemsPanel.SetActive(true);
    }
    public void BuySkinPanelOn()
    {
        BuyItemsPanel.SetActive(false);
        BuySkinsPanel.SetActive(true);
    }
    public void returnBuyPanel()
    {
        BuyPanels.SetActive(false);
        BuyItemPanelOn();
        StartStoreOb.SetActive(true);
    }
    public void SellPanelOn()
    {
        StartStoreOb.SetActive(false);
        SellPanel.SetActive(true);
    }
    public void returnSellPanel()
    {
        SellPanel.SetActive(false);
        StartStoreOb.SetActive(true);
    }
    public void ExitStore()
    {
        PlayerControl.StorePressed = false;
        StoreOb.SetActive(false);
        BuyPanels.SetActive(false);
        SellPanel.SetActive(false);
        StartStoreOb.SetActive(true);
        ClickToTalk.SetActive(true);
    }
}
