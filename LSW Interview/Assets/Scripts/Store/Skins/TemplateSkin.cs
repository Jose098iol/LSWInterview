using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Skins", menuName = "Skins Store")]

public class TemplateSkin : ScriptableObject
{
    public string nameSkin;
    public Image iconSkin;
    public int priceSkin;
}
