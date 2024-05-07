using DG.Tweening;
using UnityEngine;

public class MergeEffect : Effect
{
    [SerializeField] private Transform lightObj, ring;
    [SerializeField] private float lightDuration, ringDuration, ringScale, lightAngle;

    public override void Apply(Transform target)
    {
        // setup
        transform.SetParent(target);
        transform.localPosition = Vector3.zero;
        ring.localScale = Vector3.zero;
        ring.gameObject.SetActive(true);
        lightObj.localRotation = Quaternion.identity;
        gameObject.SetActive(true);

        // effect
        ring.DOScale(Vector3.one * ringScale, ringDuration).OnComplete(() => {
        ring.gameObject.SetActive(false);
        });
        lightObj.DORotate(new Vector3(0, 0, lightAngle), lightDuration, RotateMode.LocalAxisAdd)
            .SetLoops(0).OnComplete(() =>
            {
                MoveToPool();
            });
    }
}
