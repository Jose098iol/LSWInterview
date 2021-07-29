using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveCurrentSkin : MonoBehaviour
{
    [SerializeField] private List<GameObject> SkinPrefabs = new List<GameObject>();
    private PlayerControl PlayerControl;
    private string skinSelected = "Man";
    private int currentSkinInt;
    private GameObject currentSkin;
    private void Start()
    {
        PlayerControl = GetComponent<PlayerControl>();

        skinSelected = PlayerPrefs.GetString("CurrentSkin", "Man");

        updateSkin(skinSelected);
    }
    public void updateSkin(string newSkinSelected)
    {
        if(currentSkin != null) { Destroy(currentSkin); }

        if (newSkinSelected == "Man") { currentSkinInt = 0; }
        if (newSkinSelected == "Woman") { currentSkinInt = 1; }
        if(newSkinSelected == "Seller") { currentSkinInt = 2; }

        PlayerPrefs.SetString("CurrentSkin", newSkinSelected);

        CreateCurrentSkin();
    }
    private void CreateCurrentSkin()
    {
        currentSkin = Instantiate(SkinPrefabs[currentSkinInt], transform);
        SpriteRenderer CurrentSprite = currentSkin.GetComponent<SpriteRenderer>();
        PlayerControl.ObtainCurrentSprite(CurrentSprite);
        Animator CurrentAnim = currentSkin.GetComponent<Animator>();
        PlayerControl.ObtainCurrentAnim(CurrentAnim);
    }
}
