using UnityEngine;

public class Effect : MonoBehaviour, IObjectPooler
{
    [field: SerializeField] public string TagInPool { get; set; }

    public virtual void Apply(Transform target)
    {

    }
    public bool MoveToPool()
    {
        return ObjectPooler.Instance.PutInPool(this.TagInPool, gameObject);
    }
}
