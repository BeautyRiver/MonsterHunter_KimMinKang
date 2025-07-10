using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private RectTransform rectTransform; // RectTransform을 참조할 변수
    [SerializeField] private Vector2 ranPosRange;
    private void Awake()
    {
        // Awake에서 RectTransform 컴포넌트 참조
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        // RectTransform의 anchoredPosition을 사용하여 UI 좌표계에서 위치 설정
        // 캔버스 설정에 따라 적절한 범위를 지정해야 함
        rectTransform.anchoredPosition = new Vector2(Random.Range(-ranPosRange.x, ranPosRange.x), Random.Range(-ranPosRange.y, ranPosRange.y));
    }
}
