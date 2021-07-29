using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu ( fileName = "Items", menuName = "Items Store")]

public class TemplateItem : ScriptableObject
{
    public string nameItem;
    public Image iconItem;
    public int priceItem;
    public int amountItem;
}
