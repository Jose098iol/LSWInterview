using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collectible : MonoBehaviour
{
    public Transform pivotPlayerLeft;
    public Transform pivotPlayerRight;

    private GameObject PlayerOb;
    private Transform Player;
    private PlayerControl PlayerControl;
    private PlayerItems PlayerItems;

    private Animator CollectibleAnim;

    private int nextSpawbCollectiblePrefab;
    [SerializeField] private GameObject currentCollectiblePrefab;
    [SerializeField] private GameObject ClickTo;
    [SerializeField] private float TimeToCollect;
    [SerializeField] private float TimeToReborn;
    [SerializeField] private GameObject Collectiblebutton;
    [SerializeField] private int regawardCollected;
    private float collectingTime;
    private float CurrentRebornTime;
    private bool Collecting;
    private bool StorePressed;

    public bool availableToCollect = true;
    private bool collectibleOff = false;

    private bool Reborn;

    private void Start()
    {
        PlayerOb = GameObject.FindGameObjectWithTag("Player");
        Player = PlayerOb.GetComponent<Transform>();
        PlayerControl = PlayerOb.GetComponent<PlayerControl>();
        PlayerItems = PlayerOb.GetComponent<PlayerItems>();
        CollectibleAnim = GetComponent<Animator>();
        nextSpawbCollectiblePrefab = 1;
        DetectCurrentCollectible();
    }
    private void DetectCurrentCollectible()
    {
        string currentCollectible = gameObject.tag;

        if (currentCollectible == "Tree")
        { ClickTo.GetComponent<TextMeshProUGUI>().text = "Click to cut";  }
        if (currentCollectible == "Crystal") {
            ClickTo.GetComponent<TextMeshProUGUI>().text = "Click to mine gem"; }
        if (currentCollectible == "Rock") {
            ClickTo.GetComponent<TextMeshProUGUI>().text = "Click to sting"; }
    }
    public void CollectiblePressed()
    {
        StorePressed = PlayerControl.StorePressed;
        if (StorePressed) { return; }

        Collecting = PlayerControl.Collecting;
        if(Collecting) { return; }

        float xPosPlayer = Player.transform.position.x;
        if (xPosPlayer > transform.position.x)
        {
            PlayerControl.MovePlayerToCollectible(pivotPlayerRight.position.x, pivotPlayerRight.position.y);
            PlayerControl.ForLeft = false;
            PlayerControl.ForRight = true;
        }
        if (xPosPlayer < transform.position.x)
        {
            PlayerControl.MovePlayerToCollectible(pivotPlayerLeft.position.x, pivotPlayerLeft.position.y);
            PlayerControl.ForRight = false;
            PlayerControl.ForLeft = true;
        }

        PlayerControl.CollectiblePressed = true;
        PlayerControl.TempCollectible = this;
    }

    private void LateUpdate()
    {
        if (!collectibleOff)
        {
            if (availableToCollect)
            {
                float distancePlayer = Vector2.Distance(Player.position, transform.position);
                if (distancePlayer < 6f)
                {
                    ClickTo.SetActive(true);
                }
                else
                {
                    ClickTo.SetActive(false);
                }
            }
            else
            {
                ClickTo.SetActive(false);
                Collectiblebutton.SetActive(false);

                if (collectingTime < TimeToCollect)
                {
                    collectingTime += 1f * Time.deltaTime;
                    CollectibleAnim.SetFloat("CollectingTime", collectingTime);

                    if(collectingTime >= nextSpawbCollectiblePrefab)
                    {
                        SpawnCollectible();
                        nextSpawbCollectiblePrefab += 1;
                    }
                    if (collectingTime >= TimeToCollect)
                    {
                        Collect();
                    }
                }
            }
        }
        else
        {
            if (!Reborn)
            {
                if (CurrentRebornTime < TimeToReborn)
                {
                    CurrentRebornTime += 1f * Time.deltaTime;
                    if (CurrentRebornTime >= TimeToReborn)
                    {
                        CurrentRebornTime = 5;
                        TimeToReborn = 0;
                        Reborn = true;
                    }
                }
            }
            else
            {
                if (CurrentRebornTime > TimeToReborn)
                {
                    CurrentRebornTime -= 1f * Time.deltaTime;
                    CollectibleAnim.SetFloat("CollectingTime", CurrentRebornTime);
                    if (CurrentRebornTime <= TimeToReborn)
                    {
                        CurrentRebornTime = 0;
                        TimeToReborn = 30;
                        Collectiblebutton.SetActive(true);
                        availableToCollect = true;
                        Reborn = false;
                        collectibleOff = false;
                    }

                }
            
            }
        }
    }
    private void SpawnCollectible()
    {
        GameObject Collectible = Instantiate(currentCollectiblePrefab, transform.position, Quaternion.identity);

        moreCollectible();
    }
    private void moreCollectible()
    {
        string currentCollectible = gameObject.tag;
        if (currentCollectible == "Tree")
        {
            PlayerItems.woodCutted(regawardCollected);
        }
        if (currentCollectible == "Crystal")
        {
            PlayerItems.crystalCutted(regawardCollected);
        }
        if (currentCollectible == "Rock")
        {
            PlayerItems.rockCutted(regawardCollected);
        }
    }
    private void Collect()
    {
        collectibleOff = true;
        collectingTime = 0;
        nextSpawbCollectiblePrefab = 1;
        PlayerControl.CollectingOff();
    }

}
