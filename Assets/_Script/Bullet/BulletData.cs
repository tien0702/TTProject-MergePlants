using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BulletData : IObjectPooler
{
    private static readonly string tagInPool = "Bullet";
    public string TagInPool
    {
        set
        {
            value = tagInPool;
        }
        get
        {
            return tagInPool;
        }
    }

    [field: SerializeField] public Sprite Portrait { set; get; }
    [field: SerializeField] public Gradient TrailGradient { set; get; }
    public static float Speed = 3f;
    public Ability Ability { set; get; }
    public int Attack { set; get; }
}
