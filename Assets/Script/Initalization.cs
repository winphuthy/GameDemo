using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initalization : MonoBehaviour
{
    // Start is called before the first frame update
    public Dictionary<Vector2Int, Tile> TileTable = new Dictionary<Vector2Int, Tile>();

    public Dictionary<Vector2Int, Unit> UnitTable = new Dictionary<Vector2Int, Unit>();

    public bool ShowGridNumb;
    public GameObject Root;
    public GameObject DebugMesh;
    public PolygonCollider2D CameraLimit;
    private Vector2Int LastVector2Int;
    public float MapExtensionSize;

    void Awake()
    {
        ShowGridNumb = false;
        GetTileTable();
        GetUnitTable();

        try
        {
            // Transform a = Root.transform.Find("Root/DebugGroup");
            Root = GameObject.Find("Root");
            DebugMesh = Root.transform.Find("DebugGroup").gameObject;
            CameraLimit = Root.transform.Find("Environment/2D Collider").gameObject.GetComponent<PolygonCollider2D>();
        }
        catch (NullReferenceException e)
        {
            Debug.Log("DebugMesh did not been found!");
            ShowGridNumb = false;
            Console.WriteLine(e);
            throw;
        }

        CameraColliderSetting();
    }

    void Update()
    {
        if (ShowGridNumb)
        {
            DebugMesh.SetActive(ShowGridNumb);
            Navigation.instance.ShowGridNumb = ShowGridNumb;
            Navigation.instance.DebugMesh = DebugMesh;
        }

    }


    /// <summary>
    /// get all unit position information 
    /// </summary>
    private void GetUnitTable()
    {
        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            UnitTable.Add(unit.position, unit);
        }

        MapModel.instance.UnitTable = UnitTable;
    }


    private void GetTileTable()
    {
        Vector2Int instance = new Vector2Int();
        foreach (Tile tile in FindObjectsOfType<Tile>())
        {
            // print($"key:{tile.position},Value:{tile}");
            TileTable.Add(tile.position, tile);
            if (tile.position.x > instance.x && tile.position.y > instance.y)
            {
                instance = tile.position;
            }
        }

        print("TileTable count :  " + TileTable.Count);
        MapModel.instance.TileTable = TileTable;
        LastVector2Int = instance;
    }

    void CameraColliderSetting()
    {
        MapModel.instance.LasrVector2Int = LastVector2Int;
        //fill the 4 point which outside the usable tile
        CameraLimit.points = new Vector2[]
        {
            new Vector2((0 - MapExtensionSize), (0 - MapExtensionSize)),
            new Vector2(LastVector2Int.x + MapExtensionSize, (0 - MapExtensionSize)),
            new Vector2(LastVector2Int.x + MapExtensionSize, LastVector2Int.y + MapExtensionSize),
            new Vector2((0 - MapExtensionSize), LastVector2Int.y + MapExtensionSize),
        };
    }
}