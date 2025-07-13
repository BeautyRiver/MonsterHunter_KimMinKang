using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    [field: SerializeField] public string MonsterName { get; private set; }
    [field: SerializeField] public float MaxHP { get; private set; }
    [field: SerializeField] public float Armor { get; private set; }
    [field: SerializeField] public float CurHp { get; private set; }
    [field: SerializeField] public int GoldValue { get; private set; }

    private bool isDead = false;
    private bool isOnceRun = true;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Init();
    }
    private void OnEnable()
    {
        Init();
    }
 
    public void Init()
    {
        isDead = false;
        isOnceRun = true;
        CurHp = MaxHP;
    }

    public void OnHit(float damage)
    {
        if (!isDead)
        {
            CurHp -= damage;
            if (CurHp <= 0)
            {
                CurHp = 0;
                GetGold();
                isDead = true;
            }

            animator.Play("Hit", 0, 0f);
        }

        if (isDead && isOnceRun)
        {
            isOnceRun = false;
            Debug.Log("Slime is Dead!");
            animator.SetTrigger("Dead");
        }
    }

    public void GetGold()
    {
        var obj = PoolManager.instance.GetFromPool("GetGoldText");
        Text goldText = obj.GetComponent<Text>();
        goldText.fontSize = 100;
        goldText.color = Color.yellow;
        goldText.text = $"+{GoldValue} G";

        GameManager.instance.AddPlayerGold(GoldValue);
    }

    public void Dead()
    {
        gameObject.SetActive(false);

    }

    public void GameClear()
    {
        SceneManager.LoadScene("Clear");
    }
}
