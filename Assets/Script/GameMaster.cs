using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, Inherited = false)]
public class DisplayOnlyAttribute : PropertyAttribute
{
}

public class GameMaster : MonoBehaviour
{
    [DisplayOnly] public Unit selectedUnit;
    public Ray ClickCheck;
    private bool ShowGridNumb;
    private GameObject Root;
    private GameObject DebugMesh;
    private PolygonCollider2D CameraLimit;
    private Vector2Int LastVector2Int;
    public float MapExtensionSize;


    [SerializeField]
    public enum Turn
    {
        Player = 0,
        Ally = 1,
        Enemy = 2,
        Neutrality = 3
    }

    public Turn PlayerTurn;

    public GameObject selectedUnitSquare;


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

    void Start()
    {
        PlayerTurn = Turn.Player;
        //register view
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickCheck = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            RaycastHit2D[] hit = Physics2D.RaycastAll(new Vector2(ClickCheck.origin.x, ClickCheck.origin.y), Vector2.zero);
            foreach (var hit2D in hit)
            {
                if (hit2D.collider.gameObject)
                {
                    print(hit2D.collider);
                }
            }
            
        }

        if (selectedUnit != null)
        {
            selectedUnitSquare.SetActive(true);
            selectedUnitSquare.transform.position = selectedUnit.transform.position;
        }
        else
        {
            selectedUnitSquare.SetActive(false);
        }
    }

    public void ResetTiles()
    {
        foreach (var tile in MapModel.instance.TileTable)
        {
            tile.Value.Reset();
        }

        foreach (var textMesh in FindObjectsOfType<TextMesh>())
        {
            textMesh.text = "0";
        }
    }

    private void GetUnitTable()
    {
        MapModel.instance.UnitTable = new Dictionary<Vector2Int, Unit>();
        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            MapModel.instance.UnitTable.Add(unit.position, unit);
        }
    }


    private void GetTileTable()
    {
        Vector2Int instance = new Vector2Int();
        foreach (Tile tile in FindObjectsOfType<Tile>())
        {
            // print($"key:{tile.position},Value:{tile}");
            MapModel.instance.TileTable.Add(tile.position, tile);
            if (tile.position.x > instance.x && tile.position.y > instance.y)
            {
                instance = tile.position;
            }
        }

        print("TileTable count :  " + MapModel.instance.TileTable.Count);
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

    public void EndTurn()
    {
        Debug.Log("EndTurn");

        if (PlayerTurn == Turn.Neutrality)
        {
            PlayerTurn = Turn.Player;
        }
        else
        {
            PlayerTurn++;
        }


        if (selectedUnit != null) //if some unit has been selected
        {
            selectedUnit.selected = false; //cancel selection
            selectedUnit = null;
        }

        ResetTiles();

        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            unit.hasMoved = false; //reset all unit to movable when turn end.
            unit.AttackSquare.SetActive(false);
            unit.hasAttacked = false;
        }
    }
}