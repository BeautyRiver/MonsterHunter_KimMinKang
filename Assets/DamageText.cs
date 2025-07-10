using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private RectTransform rectTransform; // RectTransform�� ������ ����
    [SerializeField] private Vector2 ranPosRange;
    private void Awake()
    {
        // Awake���� RectTransform ������Ʈ ����
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        // RectTransform�� anchoredPosition�� ����Ͽ� UI ��ǥ�迡�� ��ġ ����
        // ĵ���� ������ ���� ������ ������ �����ؾ� ��
        rectTransform.anchoredPosition = new Vector2(Random.Range(-ranPosRange.x, ranPosRange.x), Random.Range(-ranPosRange.y, ranPosRange.y));
    }
}
