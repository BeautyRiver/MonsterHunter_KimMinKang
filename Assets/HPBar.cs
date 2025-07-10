using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Monster monster;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Text hpText;    

    public void ChangeHpBarAmount(float amount, float lerpTime)
    {
        hpText.text = $"{monster.CurHp} / {monster.MaxHP}";
        hpSlider.value = Mathf.Lerp(hpSlider.value, amount, lerpTime *  Time.deltaTime);        
    }
    

    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}
