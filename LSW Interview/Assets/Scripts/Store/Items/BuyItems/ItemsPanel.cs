using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsPanel : MonoBehaviour
{
    [SerializeField] private GameObject BuyItemPrefab;
    [SerializeField] private int amountItems;
    [SerializeField] private List <TemplateItem> Items = new List<TemplateItem>();
    
    private void Start()
    {
        for(int i = 0; i < amountItems; i++)
        {
            GameObject ItemInstantiated = Instantiate(BuyItemPrefab, transform);
            ItemInstantiated.GetComponent<Item>().CreateItem(Items[i]);
        }
    }
}
