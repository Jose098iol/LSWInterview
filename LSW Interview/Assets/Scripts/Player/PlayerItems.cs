using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerItems : MonoBehaviour
{
    public int Gold = 500;

    public int amountWood = 0;
    public int amountCrystal = 0;
    public int amountRock = 0;

    [SerializeField] private TextMeshProUGUI GoldText;
    [SerializeField] private TextMeshProUGUI WoodText;
    [SerializeField] private TextMeshProUGUI CrystalText;
    [SerializeField] private TextMeshProUGUI RockText;

    private void Start()
    {
        Gold = PlayerPrefs.GetInt("currentGold", 500);
        amountWood = PlayerPrefs.GetInt("currentWood", 0);
        amountCrystal = PlayerPrefs.GetInt("currentCrystal", 0);
        amountRock = PlayerPrefs.GetInt("currentRock", 0);

        GoldText.text = Gold.ToString();
        WoodText.text = amountWood.ToString();
        CrystalText.text = amountCrystal.ToString();
        RockText.text = amountRock.ToString();
    }
    public void woodCutted(int newWood)
    {
        amountWood += newWood;

        updateWood();
    }
    public void crystalCutted(int newCrystal)
    {
        amountCrystal += newCrystal;

        updateCrystal();
    }
    public void rockCutted(int newRock)
    {
        amountRock += newRock;

        updateRock();
    }
    public void MoreWood(int newWood, int WoodCost)
    {
        amountWood += newWood;
        Gold -= WoodCost;

        updateWood();
    }
    public void MoreCrystal(int newCrystal, int CrystalCost)
    {
        amountCrystal += newCrystal;
        Gold -= CrystalCost;

        updateCrystal();
    }
    public void MoreRock(int newRock, int RockCost)
    {
        amountRock += newRock;
        Gold -= RockCost;

        updateRock();
    }
    public void LessWood(int newWood, int WoodCost)
    {
        amountWood -= newWood;
        Gold += WoodCost;

        updateWood();
    }
    public void LessCrystal(int newCrystal, int CrystalCost)
    {
        amountCrystal -= newCrystal;
        Gold += CrystalCost;

        updateCrystal();
    }
    public void LessRock(int newRock, int RockCost)
    {
        amountRock -= newRock;
        Gold += RockCost;

        updateRock();
    }
    private void updateWood()
    {
        PlayerPrefs.SetInt("currentWood", amountWood);
        WoodText.text = amountWood.ToString();

        updateGold();
    }
    private void updateCrystal()
    {
        PlayerPrefs.SetInt("currentCrystal", amountCrystal);
        CrystalText.text = amountCrystal.ToString();

        updateGold();
    }
    private void updateRock()
    {
        PlayerPrefs.SetInt("currentRock", amountRock);
        RockText.text = amountRock.ToString();

        updateGold();
    }
    private void updateGold()
    {
        PlayerPrefs.SetInt("currentGold", Gold);
        GoldText.text = Gold.ToString();
    }
}
