using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalChanceUpItem : Item
{
    protected override void ApplyEffect()
    {
        // �� �������� ������ ȿ��        
        GameManager.instance.AddCriticalChancePer(increasePer);
    }
}