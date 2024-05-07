using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class RoadPoint
{
    public static readonly int Entry = 0;
    public static readonly int Fork1 = 1;
    public static readonly int Fork2 = 2;
    public static readonly int Exit = 3;
}

public class GardenGrid : MonoBehaviour
{
    public static GardenGrid Instance { private set; get; }
    [SerializeField] private float offsetAngleX = 32f;
    [SerializeField] private float offsetAngleY = 52.5f;
    [SerializeField] private Vector2 disPerTile = new Vector2() { x = 0.11f, y = -1.9f };
    [SerializeField] private Vector2Int gridSize = new Vector2Int { x = 3, y = 4 };
    public int GardenSize => gridSize.x * gridSize.y;
    public bool AutoMerge = false;

    [SerializeField] private ParcelOfLand parcelOfLandPrefab;
    [SerializeField] private List<ParcelOfLand> parcelOfLands = new List<ParcelOfLand>();

    // Roads
    [field: SerializeField] public Transform[] Road = new Transform[4];

    private void Awake()
    {
        Instance = this;
    }

    private void OnValidate()
    {
        if (parcelOfLands.Count == 0) DrawGarden();
        SetPositionForTiles();
    }

    void DrawGarden()
    {
        foreach (Transform child in transform)
        {
            EditorApplication.delayCall += () =>
            {
                DestroyImmediate(child.gameObject);
            };
        }
        parcelOfLands.Clear();
        int indexTile = 0;
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                var tile = Instantiate(parcelOfLandPrefab, transform);
                tile.gameObject.name = string.Format("T [{0}, {1}]", x, y);
                indexTile++;
                parcelOfLands.Add(tile);
            }
        }
    }

    void SetPositionForTiles()
    {
        int indexTile = 0;
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                parcelOfLands[indexTile].transform.localPosition = CalculatePosition(new Vector2Int(x, y));
                indexTile++;
            }
        }
    }

    Vector2 CalculatePosition(Vector2Int posInGrid)
    {
        Vector2 originalVector;
        originalVector.x = (posInGrid.x) + disPerTile.x * posInGrid.x;
        originalVector.y = (posInGrid.y) + disPerTile.y * posInGrid.y;
        float rotatedX = Mathf.Cos(offsetAngleX * Mathf.Deg2Rad) * originalVector.x - Mathf.Sin(offsetAngleY * Mathf.Deg2Rad) * originalVector.y;
        float rotatedY = Mathf.Sin(offsetAngleX * Mathf.Deg2Rad) * originalVector.x + Mathf.Cos(offsetAngleY * Mathf.Deg2Rad) * originalVector.y;

        Vector2 position = new Vector2(rotatedX, rotatedY);

        return position;
    }

    public List<ParcelOfLand> GetEmptyLands()
    {
        return parcelOfLands.Where(p => !p.Occupied).ToList();
    }

    public List<Flower> GetFlowersInGarden()
    {
        var flowers = new List<Flower>();
        parcelOfLands.ForEach(f =>
        {
            if (f.Occupied)
            {
                flowers.Add(f.FlowerOnLand);
            }
        });

        return flowers;
    }
    #region FX
    public void TurnOnFxMerge(Flower target)
    {
        var lands = parcelOfLands.Where(l =>
        {
            var flower = l.GetComponentInChildren<Flower>();
            return (flower != null) && (flower.Data.Level == target.Data.Level) && (flower != target);
        }
        ).ToList();
        lands.ForEach(l => l.DrawMergerFx(true));
    }
    public void TurnOffFxMerge()
    {
        parcelOfLands.ForEach(p => p.DrawMergerFx(false));
    }

    #endregion
}
