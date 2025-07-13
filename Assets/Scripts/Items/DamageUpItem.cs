using UnityEngine;

public class DamageUpItem : Item
{
    protected override void ApplyEffect()
    {
        // 이 아이템의 고유한 효과
        float increaseAmount = GameManager.instance.playerInfo.Damage * 0.1f;
        GameManager.instance.AddPlayerDamage(increaseAmount);
    }
}