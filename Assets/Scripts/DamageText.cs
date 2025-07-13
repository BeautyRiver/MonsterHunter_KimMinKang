using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DamageText : MonoBehaviour
{
    private Transform parent;
    private RectTransform rectTransform; 
    [SerializeField] private Vector2 ranPosRange;
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
        transform.DOScale(Vector3.one, anmationTime).SetEase(Ease.OutBack);
        rectTransform.anchoredPosition = new Vector2(Random.Range(-ranPosRange.x, ranPosRange.x), Random.Range(-ranPosRange.y, ranPosRange.y));
    }

    private IEnumerator CorDamageText()
    {
        yield return new WaitForSeconds(0.5f);
        transform.DOScale(Vector3.zero, anmationTime).SetEase(Ease.InSine).OnComplete(() => gameObject.SetActive(false));
        
    }
}
