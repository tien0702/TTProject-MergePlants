using System.Linq;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    static GameObject trashCan;

    private LayerMask layerTarget, trashLayer;
    private Camera cam;
    Vector2 mousePositionOffset;
    ParcelOfLand parcelOfLand;

    private void Awake()
    {
        if (trashCan == null)
        {
            trashCan = GameObject.Find("Trashcan");
            trashCan.SetActive(false);
        }
        layerTarget = LayerMask.NameToLayer("ParcelOfLand");
        trashLayer = LayerMask.NameToLayer("TrashCan");
    }

    private void OnEnable()
    {
        cam = Camera.main;
    }

    private void OnMouseDown()
    {
        trashCan.SetActive(true);
        mousePositionOffset = (Vector2)transform.position - GetMousePositionWorld();
        parcelOfLand = transform.parent.GetComponent<ParcelOfLand>();

        GardenGrid.Instance.GetFlowersInGarden()
            .ForEach(f => f.SetBoxColliderActive(false));
        GardenGrid.Instance.TurnOnFxMerge(parcelOfLand.FlowerOnLand);
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePositionWorld() + mousePositionOffset;
    }

    private void OnMouseUp()
    {
        GardenGrid.Instance.TurnOffFxMerge();
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), -Vector3.forward);
        parcelOfLand.FlowerOnLand.MoveLocalToOrigin(0.3f);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer.Equals(trashLayer))
            {
                parcelOfLand.FlowerOnLand.MoveToPool();
            }
            else if (hit.collider.gameObject.layer.Equals(layerTarget))
            {
                var landTarget = hit.collider.gameObject.GetComponent<ParcelOfLand>();
                if (landTarget != parcelOfLand)
                {
                    HandleLands(landTarget, parcelOfLand);
                }
            }
        }
        GardenGrid.Instance.GetFlowersInGarden()
            .ForEach(f => f.SetBoxColliderActive(true));
        trashCan.SetActive(false);
    }

    void HandleLands(ParcelOfLand landTarget, ParcelOfLand parcelOfLand)
    {
        if (!landTarget.Occupied)
        {
            landTarget.PutFlowerOnLand(parcelOfLand.TakeFlowerOnLand());
            return;
        }

        var flowerOnLandTarget = landTarget.TakeFlowerOnLand();
        // merge
        if (flowerOnLandTarget.Data.Level == parcelOfLand.FlowerOnLand.Data.Level)
        {
            int currentLv = flowerOnLandTarget.Data.Level;
            parcelOfLand.TakeFlowerOnLand().MoveToPool();
            flowerOnLandTarget.MoveToPool();
            var nextFlower = FlowerManager.GetInstance().GetNextLevel(currentLv);
            landTarget.PutFlowerOnLand(nextFlower);
            nextFlower.transform.localPosition = Vector3.zero;

            var obj = ObjectPooler.Instance.GetObject("Merge");
            var eff = obj.GetComponent<Effect>();
            eff.Apply(nextFlower.transform);
            AudioManager.Instance.PlaySFX("merge");
        }
        // 
        else
        {
            var flowerOnLand = parcelOfLand.TakeFlowerOnLand();
            landTarget.PutFlowerOnLand(flowerOnLand);
            parcelOfLand.PutFlowerOnLand(flowerOnLandTarget);

            flowerOnLand.MoveLocalToOrigin(0.3f);
            flowerOnLandTarget.MoveLocalToOrigin(0.3f);
        }
    }

    Vector2 GetMousePositionWorld()
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }
}
