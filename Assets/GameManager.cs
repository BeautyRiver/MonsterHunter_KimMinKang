using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Monster[] slimes;
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private float damage;
    private Monster curSlime;

    public void SpawnSlime()
    {
        int spawnIndex = Random.Range(0, slimes.Length);
        GameObject newSlime = Instantiate(slimes[spawnIndex].gameObject);
        curSlime = newSlime.GetComponent<Monster>();
    }

    private void Update()
    {
        if (curSlime == null)
            SpawnSlime();
    }

    public void HitSlime()
    {
        if (curSlime == null)
            return;

        curSlime.OnHit(damage);
        poolManager.GetDamageText(damage);
    }
}
