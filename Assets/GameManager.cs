using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public PlayerInfo playerInfo;    
    [SerializeField] private Monster[] slimes;
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private int curStage;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button nextButton;
    private Monster curSlime;

    private void Update()
    {
        if (curSlime == null)
            SpawnSlime();        
    }
    public void SpawnSlime()
    {
        if (curSlime != null)        
            Destroy(curSlime.gameObject);            
        

        GameObject newSlime = Instantiate(slimes[curStage].gameObject);
        curSlime = newSlime.GetComponent<Monster>();
    }

    public void HitSlime()
    {
        if (curSlime == null || curSlime.CurHp <= 0)
            return;

        curSlime.OnHit(playerInfo.Damage);
        poolManager.GetDamageText(playerInfo.Damage);
    }

    public void PrevMonster()
    {
        curStage = Mathf.Max(0, curStage - 1);        
        SpawnSlime();
    }
    public void NextMonster()
    {
        curStage = Mathf.Min(curStage + 1, slimes.Length - 1);
        SpawnSlime();
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }
}

[Serializable]
public struct PlayerInfo
{
    [field: SerializeField] public float Damage { get; set; }

    [field: SerializeField] public float ArmorPenetration { get; set; }

    [field: SerializeField] public float CriticalChance { get; set; }

    [field: SerializeField] public float CriticalDamage { get; set; }
}