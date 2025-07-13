using UnityEngine;
using UnityEngine.UI;

// 1. "�̿ϼ� ���赵"���� �˸��� abstract Ű���� �߰�
public abstract class Item : MonoBehaviour
{
    // ... (���� ������ �״��) ...
    public string itemName;
    [TextArea] public string itemDesc;
    public float increasePer;
    public float increaseValuePer;
    public int needGoldValue;
    public GameObject goldLackInfo;
    public Text itemNameText;
    public Text itemDescText;
    public Text needGoldValueText;

    protected void Start()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        itemNameText = texts[0];
        itemNameText.text = itemName;
        itemDescText = texts[1];
        itemDescText.text = itemDesc + increasePer.ToString("F1") + "%";
        needGoldValueText = texts[2];
        needGoldValue = PlayerPrefs.GetInt(itemName, needGoldValue);
        needGoldValueText.text = needGoldValue.ToString() + "G";
    }

    public void BuyItem()
    {
        if (GameManager.instance.playerInfo.PlayerGold < needGoldValue)
        {
            Debug.Log("������");
            goldLackInfo.SetActive(true); // ��� ���� UI ǥ�� ����
            return;
        }

        GameManager.instance.AddPlayerGold(-needGoldValue);

        ApplyEffect();

        needGoldValue = Mathf.RoundToInt(needGoldValue * increaseValuePer);
        needGoldValueText.text = needGoldValue + "G";
        PlayerPrefs.SetInt(itemName, needGoldValue);
    }

    protected abstract void ApplyEffect();
}