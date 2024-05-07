using System.Collections;
using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{
    [field: SerializeField] public float TimeSpawnFlower { set; get; }
    [field: SerializeField] public float ElapsedTime { set; get; }
    [field: SerializeField] public int ReduceLevelSpawn { private set; get; }
    void Start()
    {
        ElapsedTime = 0;
    }

    private void Update()
    {
        ElapsedTime += Time.deltaTime;
        if (ElapsedTime >= TimeSpawnFlower)
        {
            ElapsedTime = 0;
            var land = RandomLand();
            if (land == null)
            {
                //Debug.Log("Not enough slot");
            }
            else
            {
                var flower = GetFlowerSpawn();
                land.PutFlowerOnLand(flower);
                flower.transform.localPosition = Vector3.zero;

                // effect
                var appearEf = ObjectPooler.Instance.GetObject("Appear").GetComponent<AppearEffect>();
                appearEf.gameObject.SetActive(true);
                appearEf.transform.SetParent(flower.transform);
                appearEf.transform.localPosition = Vector3.zero;
            }
        }
    }

    IEnumerator SpawnFlower()
    {
        yield return new WaitForSeconds(TimeSpawnFlower);


        StartCoroutine(SpawnFlower());
    }

    ParcelOfLand RandomLand()
    {
        var emptylands = GardenGrid.Instance.GetEmptyLands();
        return (emptylands?.Count > 0) ? (emptylands[UnityEngine.Random.Range(0, emptylands.Count)]) : (null);
    }

    Flower GetFlowerSpawn()
    {
        int level = Mathf.Max(1, FlowerManager.GetInstance().MaxLevelUnlocked - ReduceLevelSpawn);
        return FlowerManager.GetInstance().GetByLevel(level);
    }
}
