using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalDamageUpItem : Item
{
    protected override void ApplyEffect()
    {
        // �� �������� ������ ȿ��
        GameManager.instance.AddCriticalDamagePer(increasePer);
    }
}