using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsPanel : MonoBehaviour
{
    [SerializeField] private GameObject BuySkinPrefab;
    [SerializeField] private int amountSkins;
    [SerializeField] private List<TemplateSkin> Skins = new List<TemplateSkin>();

    [SerializeField] private List<Skin> AvailableSkins = new List<Skin>();

    private void Start()
    {
        for (int i = 0; i < amountSkins; i++)
        {
            GameObject ItemInstantiated = Instantiate(BuySkinPrefab, transform);
            ItemInstantiated.GetComponent<Skin>().CreateItem(Skins[i]);
            AvailableSkins.Add(ItemInstantiated.GetComponent<Skin>());
        }
    }

    public void RestartButtons()
    {
        foreach(Skin skin in AvailableSkins)
        {
            skin.initialState();
        }
    }

}
