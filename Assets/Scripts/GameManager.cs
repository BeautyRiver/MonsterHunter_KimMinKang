using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private int curStage;

    [SerializeField] private List<string> monsterTags;

    [SerializeField] private Text playerInfoText;
    [SerializeField] private Text slimeNameText;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Text hpText;

    [SerializeField] private Button prevButton;
    [SerializeField] private Button nextButton;

    [SerializeField] private GameObject storeUi;

    private Monster curMonster;


    private void Start()
    {
        for (int i = 2; i < PoolManager.instance.pools.Count; i++)
        {
            monsterTags.Add(PoolManager.instance.pools[i].tag);
        }
        LoadPlayerInfo();
    }
    private void Update()
    {
        if (curMonster == null || !curMonster.gameObject.activeSelf)
        {
            SpawnSlime();
        }

        ChangeHpBarAmount(curMonster.CurHp / curMonster.MaxHP, 15f);
        playerInfoText.text =
            $"<color=\"green\">[PlayerInfo]</color>\r\n" +
            $"<color=\"white\">Damage:  {playerInfo.Damage.ToString("F1")}\r\n" +
            $"CriticalChance: {playerInfo.CriticalChancePer.ToString("F1")}%\r\n" +
            $"CriticalDamage: {playerInfo.CriticalDamagePer.ToString("F1")}%\r\n</color>\r\n" +
            $"<color=\"yellow\">PlayerGold: {playerInfo.PlayerGold} G</color>";
    }

    public void SpawnSlime()
    {
        if (curMonster != null)
        {
            PoolManager.instance.ReturnToPool(monsterTags[curStage], curMonster.gameObject);
        }

        GameObject newSlime = PoolManager.instance.GetFromPool(monsterTags[curStage]);
        curMonster = newSlime.GetComponent<Monster>();
        slimeNameText.text = curMonster.MonsterName;
        curMonster.Init(); // 체력 초기화 등
    }

    // SpawnSlime 오버로딩
    public void SpawnSlime(int lastStageIndex)
    {
        if (curMonster != null)
        {
            PoolManager.instance.ReturnToPool(monsterTags[lastStageIndex], curMonster.gameObject);
        }

        GameObject newSlime = PoolManager.instance.GetFromPool(monsterTags[curStage]);
        curMonster = newSlime.GetComponent<Monster>();
        slimeNameText.text = curMonster.MonsterName;
        curMonster.Init();
    }

    public void HitSlime()
    {
        if (curMonster == null || curMonster.CurHp <= 0)
            return;

        bool isCritical = UnityEngine.Random.Range(0f, 100f) < playerInfo.CriticalChancePer;
        float realDamage;
        GameObject damageText = PoolManager.instance.GetFromPool("DamageText");
        Text dmgTextComponent = damageText.GetComponent<Text>();


        if (isCritical)
        {
            dmgTextComponent.fontSize = 100;
            realDamage = playerInfo.Damage * (1f + playerInfo.CriticalDamagePer / 100f);
            dmgTextComponent.text = $"-{Mathf.RoundToInt(realDamage)}!!";
            dmgTextComponent.color = Color.green;
        }
        else
        {
            dmgTextComponent.fontSize = 60;
            realDamage = playerInfo.Damage;
            dmgTextComponent.text = $"-{Mathf.RoundToInt(realDamage)}";
            dmgTextComponent.color = Color.white;
        }

        SoundManager.instance.PlaySound("Hit");
        curMonster.OnHit(realDamage);
    }

    public void ChangeHpBarAmount(float amount, float lerpTime)
    {
        hpText.text = $"{curMonster.CurHp.ToString("F1")} / {curMonster.MaxHP}";
        hpSlider.value = Mathf.Lerp(hpSlider.value, amount, lerpTime * Time.deltaTime);
    }

    public void PrevMonster()
    {
        int lastStage = curStage; // 반납할 몬스터의 태그를 위해 이전 스테이지 저장
        curStage = Mathf.Max(0, curStage - 1);
        SpawnSlime(lastStage); // SpawnSlime을 호출하여 몬스터 교체

        nextButton.interactable = true;
        if (curStage <= 0)
            prevButton.interactable = false;
    }

    public void NextMonster()
    {
        int lastStage = curStage; // 반납할 몬스터의 태그를 위해 이전 스테이지 저장
        curStage = Mathf.Min(curStage + 1, monsterTags.Count - 1);
        SpawnSlime(lastStage); // SpawnSlime을 호출하여 몬스터 교체

        prevButton.interactable = true;
        if (curStage >= monsterTags.Count - 1)
            nextButton.interactable = false;

    }

    public void LoadPlayerInfo()
    {

        playerInfo.Damage = PlayerPrefs.GetFloat("playerDamage", 10f);
        playerInfo.CriticalChancePer = PlayerPrefs.GetFloat("playerCriticalChancePer", 5f);
        playerInfo.CriticalDamagePer = PlayerPrefs.GetFloat("playerCriticalDamagePer", 10f);
        playerInfo.PlayerGold = PlayerPrefs.GetInt("playerGold", 0);
    }

    public void SavePlayerInfo()
    {
        PlayerPrefs.SetFloat("playerDamage", playerInfo.Damage);
        PlayerPrefs.SetFloat("playerCriticalChancePer", playerInfo.CriticalChancePer);
        PlayerPrefs.SetFloat("playerCriticalDamagePer", playerInfo.CriticalDamagePer);
        PlayerPrefs.SetInt("playerGold", playerInfo.PlayerGold);
    }

    public void AddPlayerDamage(float damage)
    {
        playerInfo.Damage += damage;
        SavePlayerInfo();
    }

    public void AddCriticalChancePer(float criticalChance)
    {
        playerInfo.CriticalChancePer += criticalChance;
        SavePlayerInfo();
    }

    public void AddCriticalDamagePer(float criticalDamage)
    {
        playerInfo.CriticalDamagePer += criticalDamage;
        SavePlayerInfo();
    }

    public void AddPlayerGold(int gold)
    {
        playerInfo.PlayerGold += gold;
        SavePlayerInfo();
    }

    public void OpenStore(bool isOpen)
    {
        if (isOpen)
        {
            //Pause();
            storeUi.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBack).SetUpdate(true);
        }
        else
        {
            //Resume();
            storeUi.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InBack).SetUpdate(true);
        }
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
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

[System.Serializable]
public struct PlayerInfo
{
    [field: SerializeField] public float Damage { get; set; }
    [field: SerializeField] public float CriticalChancePer { get; set; }
    [field: SerializeField] public float CriticalDamagePer { get; set; }

    [SerializeField] private int _playerGold;

    public int PlayerGold
    {
        get { return _playerGold; }
        set { _playerGold = value <= 0 ? 0 : value; } // value가 0보다 작으면 0을, 아니면 value를 저장
    }
}