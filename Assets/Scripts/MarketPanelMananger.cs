using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketPanelMananger : MonoBehaviour
{
    public GameObject content;
    public Button[] skinButtons;
    public IAPMgr iAPMgr;

    // Start is called before the first frame update
    void OnEnable()
    {
        skinButtons = content.GetComponentsInChildren<Button>();
        StartCoroutine(CheckPurchasingSkin());
    }

    // Update is called once per frame
    void Update()
    {
        
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

    void PurchasingSkin(Button button)
    {
        button.interactable = false;
        button.GetComponentsInChildren<Text>()[1].text = "이미 구매";
    }
}
