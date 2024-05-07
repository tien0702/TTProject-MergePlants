using System.Linq;
using UnityEngine;

public static class DetectZombie
{
    public static Zombie tempZombie;
    public static Zombie DetectNearest(Transform transform, float range)
    {
        var zombies = ZombieManager.GetInstance().ZombiesInGarden();
        if(zombies == null || zombies.Count == 0) return null;
        zombies = zombies.Where(z => Vector2.Distance(z.transform.position, transform.position) <= range).ToList();

        if (zombies == null || zombies.Count == 0) return null;
        int index = 0;
        float disNearest = 0;
        for (int i = 0; i < zombies.Count; i++)
        {
            float distance = Vector2.Distance(transform.position, zombies[i].transform.position);
            if (distance < disNearest)
            {
                disNearest = distance;
                index = i;
            }
        }

        return zombies[index];
    }
}
