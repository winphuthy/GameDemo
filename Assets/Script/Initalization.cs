using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initalization : MonoBehaviour
{
    // Start is called before the first frame update
    public Dictionary<Vector2Int, Tile> TileTable = new Dictionary<Vector2Int, Tile>();

    public Dictionary<Vector2Int, Unit> UnitTable = new Dictionary<Vector2Int, Unit>();


    void Start()
    {
        foreach (Tile tile in FindObjectsOfType<Tile>())
        {
            TileTable.Add(tile.position, tile);
        }
        GetUnitTable();
    }

    public void GetUnitTable()
    {
        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            UnitTable.Add(unit.position, unit);
        }
        /*foreach (Vector2Int vector2 in UnitTable.Keys)
        {
            Debug.Log(vector2.ToString());
        }*/

        //Debug.Log(UnitTable[new Vector2Int(3,5)].ToString());
    }

}
