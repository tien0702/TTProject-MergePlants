using DG.Tweening;
using UnityEngine;

public class ParcelOfLand : MonoBehaviour
{
    public bool Occupied => (FlowerOnLand != null);
    public Flower FlowerOnLand { private set; get; }

    // Components
    Transform fxGroundMerge;
    private void Awake()
    {
        fxGroundMerge = transform.Find("FxGroundMerge");
        fxGroundMerge.transform.DOScale(1.2f, 0.5f).SetEase(Ease.InFlash).SetLoops(-1, LoopType.Yoyo);

    }
    private void Start()
    {
        FlowerOnLand = GetComponentInChildren<Flower>();
    }

    private void Update()
    {

    }
    private void OnDrawGizmos()
    {
        /*Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);*/
    }

    public Flower TakeFlowerOnLand()
    {
        if (!Occupied) return null;
        var temp = FlowerOnLand;
        this.FlowerOnLand = null;
        return temp;
    }

    public void PutFlowerOnLand(Flower flower)
    {
        if (FlowerOnLand != null)
        {
            Debug.LogWarning("Cannot be put in, because the land is occupied!");
            return;
        }
        FlowerOnLand = flower;
        FlowerOnLand.transform.SetParent(transform);
    }
    public void DrawMergerFx(bool active)
    {
        fxGroundMerge.gameObject.SetActive(active);
    }
}
