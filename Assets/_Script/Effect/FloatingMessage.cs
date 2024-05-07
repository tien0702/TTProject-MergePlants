using DG.Tweening;
using UnityEngine;

public enum NumberType
{
    Normal,
    Crit,
    Coin
}

public class FloatingMessage : MonoBehaviour
{
    public static FloatingMessage Instance { private set; get; }

    [SerializeField] private float delayTime, delayCrit, duration;
    [SerializeField] private Vector3 startVal, EndVal;
    [SerializeField] private TextMesh floatText;
    [SerializeField] private GameObject goldIcon, crit;

    float elapsedTime = 0f;

    Transform holder;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }
    }

    private void Start()
    {
        holder = HolderController.Instance.GetByName("FloatTextHolder");
    }

    private void Update()
    {
        if (elapsedTime < delayTime) elapsedTime += Time.deltaTime;
    }

    public void FloatingDamage(Transform target, int dame, bool isCrit)
    {
        if (elapsedTime < delayTime && !isCrit) return;
        elapsedTime = 0;
        var position = RandomPostion();

        // bounce up crit icon
        if (isCrit)
        {
            var cr = Instantiate(crit);
            cr.transform.position = target.position;
            cr.transform.DOMove(target.transform.position + position * 1.2f, duration)
            .OnComplete(() =>
            {
                Destroy(cr.gameObject);
            });
        }

        // bounce up number text
        var text = Instantiate(floatText, holder);
        text.text = "-" + dame.ToString();
        text.transform.position = target.position;
        if (isCrit) text.color = Color.red;
        text.transform.DOMove(target.transform.position + position, duration)
            .SetDelay(0.2f)
            .OnComplete(() =>
            {
                Destroy(text.gameObject);
            });
    }

    public void FloatingGold(Transform target, int amount)
    {
        elapsedTime = 0;
        var position = RandomPostion();

        var gold = Instantiate(goldIcon);
        var text = Instantiate(floatText);

        text.text = "+" + amount.ToString();

        gold.transform.SetParent(text.transform);

        text.transform.position = target.position;
        text.color = Color.yellow;
        text.transform.DOMove(target.transform.position + position, duration)
            .OnComplete(() =>
            {
                Destroy(text.gameObject);
                Destroy(gold.gameObject);
            });
    }

    Vector3 RandomPostion()
    {
        float x = UnityEngine.Random.Range(startVal.x, EndVal.x);
        float y = UnityEngine.Random.Range(startVal.y, EndVal.y);
        var position = new Vector3(x, y, 0);

        return new Vector3(x, y, 0);
    }
}
