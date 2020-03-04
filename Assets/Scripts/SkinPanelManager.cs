using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinPanelManager : MonoBehaviour
{
    public GameObject content;
    public Button[] skinButtons;
    public IAPMgr iAPMgr;
    public GameObject cat;
    public Image[] skinImages;

    private void Awake()
    {
        skinButtons = content.GetComponentsInChildren<Button>();
        skinImages = cat.GetComponentsInChildren<Image>();
        for (int i = 1; i < skinImages.Length; i++)
            skinImages[i].gameObject.SetActive(false);
        StartCoroutine(CheckPurchasingSkin());
    }

    IEnumerator CheckPurchasingSkin()
    {
        while (!iAPMgr.IsInitalized())
        {
            MonoBehaviour.print(iAPMgr.IsInitalized());
            yield return null;
        }

        foreach (Button skinButton in skinButtons)
        {
            if (iAPMgr.HasRecipt(skinButton.name)) PurchasingSkin(skinButton);
        }
    }

    void PurchasingSkin(Button skin)
    {
        skin.interactable = true;
        skin.GetComponentsInChildren<Text>()[1].text = "";
    }

    public void PurchasingSkin(string skinID)
    {
        for(int i = 0; i < skinButtons.Length; i++)
        {
            if (skinButtons[i].name.Equals(skinID))
            {
                PurchasingSkin(skinButtons[i]);
                break;
            }
        }
    }

    public void OnClickSkinButton(int num)
    {
        for (int i = 0; i < skinButtons.Length; i++)
        {
            if (skinButtons[i].interactable) skinButtons[i].GetComponentsInChildren<Text>()[1].text = "";
        }

        skinButtons[num].GetComponentsInChildren<Text>()[1].text = "적용중";

        for (int i = 1; i < skinImages.Length; i++)
        {
            if (i == num)
            {
                skinImages[i].gameObject.SetActive(true);
                continue;
            }

            skinImages[i].gameObject.SetActive(false);
        }
    }
}
