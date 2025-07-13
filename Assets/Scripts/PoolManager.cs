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

    // 2. 오브젝트를 풀에서 가져오는 범용 함수
    public GameObject GetFromPool(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
            return null;
        }

        // 1. 리스트에서 비활성화된 오브젝트를 찾는다
        foreach (GameObject obj in poolDictionary[tag])
        {
            if (!obj.activeSelf)
            {
                obj.SetActive(true);
                return obj; // 찾으면 바로 활성화하고 반환
            }
        }

        // 2. 여기까지 왔다면, 리스트에 사용 가능한 오브젝트가 없다는 뜻.
        // 따라서 새로 하나 생성한다. (풀 자동 확장)
        foreach (Pool p in pools)
        {
            if (p.tag == tag)
            {
                GameObject newObj = Instantiate(p.prefab, transform);
                poolDictionary[tag].Add(newObj); // **중요: 새로 만든 오브젝트도 리스트에 추가!**
                return newObj;
            }
        }

        return null; // 예외 상황
    }

    public void ReturnToPool(string tag, GameObject objectToReturn)
    {
        // 태그가 존재하는지 확인하는 방어 코드 (선택 사항이지만 좋은 습관)
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
            Destroy(objectToReturn); // 풀이 없으면 그냥 파괴
            return;
        }

        // 반납의 핵심 로직! 오브젝트를 비활성화해서 다시 사용할 수 있는 상태로 만듦
        objectToReturn.SetActive(false);
    }
}
