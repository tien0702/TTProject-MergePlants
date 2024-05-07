using UnityEngine;

public class ZombieMove : MonoBehaviour
{
    int targetIndexRoad = 1;
    ZombieData zombieData;
    Transform targetPoint = null;
    Transform[] road;

    private void Awake()
    {
    }

    void Update()
    {
        if (targetPoint == null) return;
        Vector3 direction = targetPoint.position - transform.position;
        float speed = zombieData.CurrentMoveSpeed * ZombieManager.GetInstance().SpeedMultiplier;
        transform.position += direction.normalized * speed * Time.deltaTime;
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            targetPoint = NextRoadPoint();
            if(targetPoint == null)
            {

            }
            else if(targetIndexRoad == RoadPoint.Exit)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f); // flip
            }
        }
    }

    private void OnEnable()
    {
        road = GardenGrid.Instance.Road;
        zombieData = GetComponent<Zombie>().Data;
    }

    public void StartMove()
    {
        transform.position = road[0].position;
        targetIndexRoad = 1;
        targetPoint = road[targetIndexRoad];
    }

    Transform NextRoadPoint()
    {
        targetIndexRoad++;
        if(targetIndexRoad < road.Length) 
            return road[targetIndexRoad];
        return null;
    }
}
