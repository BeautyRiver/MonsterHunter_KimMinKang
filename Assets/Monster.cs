using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    [SerializeField] private HPBar hpBar;
    [SerializeField] private Text nameText;

    [field: SerializeField] public string MonsterName { get; private set; }
    [field: SerializeField] public float MaxHP { get; private set; }
    [field: SerializeField] public float Armor { get; private set; }
    [field: SerializeField] public float CurHp { get; private set; }

    private float hpBarlerpTime;
    private bool isDead = false;
    private bool isOnceRun = true;
    private Animator animator;

    private void Awake()
    {
        hpBarlerpTime = 15f;
        animator = GetComponent<Animator>();
        Init();
    }
    private void OnEnable()
    {
        Init();
    }
    private void Update()
    {
        hpBar.ChangeHpBarAmount(CurHp / MaxHP, hpBarlerpTime);
    }

    private void Init()
    {
        nameText.text = MonsterName;
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
                isDead = true;
            }
            
            animator.Play("Hit",0,0f);
        }

        if (isDead && isOnceRun)
        {
            isOnceRun = false;
            Debug.Log("Slime is Dead!");
            animator.SetTrigger("Dead");                        
        }
    }

    public void Dead()
    {
        Destroy(this.gameObject);
    }

    public void GameClear()
    {

    }
}
