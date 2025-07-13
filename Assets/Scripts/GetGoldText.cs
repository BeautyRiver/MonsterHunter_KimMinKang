using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GetGoldText : MonoBehaviour
{
    private Transform parent;
    private RectTransform rectTransform;
    [SerializeField] private float ranPosX;
    [SerializeField] private float anmationTime;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        parent = GameObject.FindGameObjectWithTag("MainUi").GetComponent<Transform>();
    }

    private void OnEnable()
    {
        SetDamageText();
        StartCoroutine(CorDamageText());
    }

    private void SetDamageText()
    {
        transform.SetParent(parent);
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, anmationTime).SetEase(Ease.InQuad);
        rectTransform.anchoredPosition = new Vector2(Random.Range(-ranPosX,ranPosX), 250);
    }

    private IEnumerator CorDamageText()
    {
        yield return new WaitForSeconds(1f);
        transform.DOScale(Vector3.zero, anmationTime).SetEase(Ease.InQuad).OnComplete(() => gameObject.SetActive(false));

    }
}
