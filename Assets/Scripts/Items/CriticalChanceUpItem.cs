using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalChanceUpItem : Item
{
    protected override void ApplyEffect()
    {
        // 이 아이템의 고유한 효과        
        GameManager.instance.AddCriticalChancePer(increasePer);
    }
}