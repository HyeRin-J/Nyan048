using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public int eraseItemAmount;
    public int upgradeItemAmount;
    public Text eraseAmountText;
    public Text upgradeAmountText;

    public GameObject itemUsePanel;
    public GameObject itemZeroAmountPanel;
    public GameObject itemEarnPanel;
    public Text itemNameTextinUsePanel;
    public Text itemNameTextinZeroAmountPanel;
    public Text itemNameTextinitemEarnPanel;

    public GameManager gameManager;
    public AdMobManager ads;

    public Text upgradeText;

    public bool isUpgradeState;

    public float clickTime;

    // Use this for initialization
    void Start()
    {
        ItemAmountSetting();
    }

    // Update is called once per frame
    void Update()
    {
        if (isUpgradeState)
        {
            clickTime += Time.deltaTime;
            if (clickTime >= .5f)
                DetectingN();
        }
    }

    public void ItemAmountSetting()
    {
        eraseItemAmount = PlayerPrefs.GetInt("EraseAmount");
        upgradeItemAmount = PlayerPrefs.GetInt("UpgradeAmount");
        eraseAmountText.text = string.Format("X {0:d2}", eraseItemAmount);
        upgradeAmountText.text = string.Format("X {0:d2}", upgradeItemAmount);
    }

    public void OnClickItemButton(string itemName)
    {
        string itemNameText = string.Format("[ {0} ]", itemName);
        switch (itemName)
        {
            case "지우개":
                if (eraseItemAmount > 0)
                {
                    itemNameTextinUsePanel.text = itemNameText;
                    itemUsePanel.SetActive(true);
                }
                else
                {
                    itemNameTextinZeroAmountPanel.text = itemNameText;
                    itemZeroAmountPanel.SetActive(true);
                }
                break;
            case "업그레이드":
                if (upgradeItemAmount > 0)
                {
                    itemNameTextinUsePanel.text = itemNameText;
                    itemUsePanel.SetActive(true);
                }
                else
                {
                    itemNameTextinZeroAmountPanel.text = itemNameText;
                    itemZeroAmountPanel.SetActive(true);
                }
                break;
        }

        PlayerPrefs.SetInt("EraseAmount", eraseItemAmount);
        PlayerPrefs.SetInt("UpgradeAmount", upgradeItemAmount);
    }

    public void OnClickUseItemButton()
    {
        switch (itemNameTextinUsePanel.text)
        {
            case "[ 지우개 ]":
                eraseItemAmount -= 1;
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                        if (gameManager.Square[i, j] != null)
                        {
                            SpriteRenderer renderer = gameManager.Square[i, j].GetComponent<SpriteRenderer>();
                            if (renderer.sprite.name.Equals("n_0") || renderer.sprite.name.Equals("n_1"))
                            {
                                Destroy(renderer.gameObject);
                            }
                        }
                }
                break;
            case "[ 업그레이드 ]":
                upgradeItemAmount -= 1;
                gameManager.PauseGame();
                upgradeText.gameObject.SetActive(true);
                isUpgradeState = true;
                clickTime = 0.0f;
                break;
        }
        PlayerPrefs.SetInt("EraseAmount", eraseItemAmount);
        PlayerPrefs.SetInt("UpgradeAmount", upgradeItemAmount);
        ItemAmountSetting();
    }

    public void OnClickAdConfirmButton()
    {
        switch (itemNameTextinZeroAmountPanel.text)
        {
            case "[ 지우개 ]":
                ads.ShowRewardAd("Erase");
                break;
            case "[ 업그레이드 ]":
                ads.ShowRewardAd("Upgrade");
                break;
        }
    }

    public void DetectingN()
    {
        Ray ray;
        RaycastHit hit = new RaycastHit();
        LayerMask mask = 1 << 8;

#if UNITY_EDITOR
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100f, mask))
        {
            if (Input.GetMouseButtonUp(0))
            {
                OnClickNIsUpgradeState(hit);
            }
        }

#elif UNITY_ANDROID
        if (Input.touchCount == 1)
        {
            Touch touch = Input.touches[0];
            MonoBehaviour.print("Touch count : " + Input.touchCount);
            if (touch.phase == TouchPhase.Ended)
            {
                // Construct a ray from the current touch coordinates
                ray = Camera.main.ScreenPointToRay(touch.position);
                MonoBehaviour.print("Touch End" + touch.position);
                if (Physics.Raycast(ray, out hit, 100f, mask))
                {
                    OnClickNIsUpgradeState(hit);
                }
            }
        }  
#endif
    }

    void OnClickNIsUpgradeState(RaycastHit hit)
    {
        int index = int.Parse(hit.collider.gameObject.name.Split('_')[1].Replace("(Clone)", ""));

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (gameManager.Square[i, j] == hit.collider.gameObject)
                {
                    gameManager.Square[i, j] = Instantiate(gameManager.n[index + 1], hit.collider.gameObject.transform.position, Quaternion.identity);
                    break;
                }
            }
        }
        Destroy(hit.collider.gameObject);
        gameManager.ReleaseGame();
        isUpgradeState = false;
        upgradeText.gameObject.SetActive(false);
    }
}
