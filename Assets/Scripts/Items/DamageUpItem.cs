using UnityEngine;

public class DamageUpItem : Item
{
    protected override void ApplyEffect()
    {
        // �� �������� ������ ȿ��
        float increaseAmount = GameManager.instance.playerInfo.Damage * 0.1f;
        GameManager.instance.AddPlayerDamage(increaseAmount);
    }
}