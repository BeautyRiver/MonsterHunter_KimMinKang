using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    [SerializeField] public List<Pool> pools;
    private Dictionary<string, List<GameObject>> poolDictionary;

    private void Start()
    {
        poolDictionary = new Dictionary<string, List<GameObject>>();

        foreach (Pool pool in pools)
        {
            List<GameObject> objectPool = new List<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                objectPool.Add(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    // 2. ������Ʈ�� Ǯ���� �������� ���� �Լ�
    public GameObject GetFromPool(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
            return null;
        }

        // 1. ����Ʈ���� ��Ȱ��ȭ�� ������Ʈ�� ã�´�
        foreach (GameObject obj in poolDictionary[tag])
        {
            if (!obj.activeSelf)
            {
                obj.SetActive(true);
                return obj; // ã���� �ٷ� Ȱ��ȭ�ϰ� ��ȯ
            }
        }

        // 2. ������� �Դٸ�, ����Ʈ�� ��� ������ ������Ʈ�� ���ٴ� ��.
        // ���� ���� �ϳ� �����Ѵ�. (Ǯ �ڵ� Ȯ��)
        foreach (Pool p in pools)
        {
            if (p.tag == tag)
            {
                GameObject newObj = Instantiate(p.prefab, transform);
                poolDictionary[tag].Add(newObj); // **�߿�: ���� ���� ������Ʈ�� ����Ʈ�� �߰�!**
                return newObj;
            }
        }

        return null; // ���� ��Ȳ
    }

    public void ReturnToPool(string tag, GameObject objectToReturn)
    {
        // �±װ� �����ϴ��� Ȯ���ϴ� ��� �ڵ� (���� ���������� ���� ����)
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
            Destroy(objectToReturn); // Ǯ�� ������ �׳� �ı�
            return;
        }

        // �ݳ��� �ٽ� ����! ������Ʈ�� ��Ȱ��ȭ�ؼ� �ٽ� ����� �� �ִ� ���·� ����
        objectToReturn.SetActive(false);
    }
}
