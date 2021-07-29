using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SellItemsPanel : MonoBehaviour
{
    [SerializeField] private GameObject SellItemPrefab;
    [SerializeField] private int amountItems;
    [SerializeField] private List<TemplateItem> Items = new List<TemplateItem>();

    private void Start()
    {
        for (int i = 0; i < amountItems; i++)
        {
            GameObject ItemInstantiated = Instantiate(SellItemPrefab, transform);
            ItemInstantiated.GetComponent<SellItem>().CreateItem(Items[i]);
        }
    }
}
