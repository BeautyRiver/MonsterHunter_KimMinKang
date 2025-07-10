using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private GameObject damageText;
    private List<GameObject> damageTexts = new List<GameObject>();
    private void Start()
    {
        SetDamageText();
    }

    public void SetDamageText()
    {
        for (int i = 0; i < 10; i++)
        {
            var obj = Instantiate(damageText, canvasTransform);
            obj.SetActive(false);
            damageTexts.Add(obj);
        }
    }

    public void GetDamageText(float damage)
    {
        GameObject tempObj = null;
        for (int i = 0; i < damageTexts.Count; i++)
        {
            if (damageTexts[i].activeSelf == false)
            {
                tempObj = damageTexts[i];
                tempObj.SetActive(true);
                tempObj.GetComponent<Text>().text = $"-{damage}!";
                return;
            }
        }
        if (tempObj == null)
        {
            SetDamageText();
            GetDamageText(damage);
        }
    }
}
