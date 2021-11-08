using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    Initalization init;

    private void Start()
    {
        init = GetComponent<Initalization>();
        init = FindObjectOfType<Initalization>();

    }

    public Unit GetUnit(Vector2Int TargetPosition)//get the unit in target position
    {
        //Debug.Log(TargetPosition);
        return init.UnitTable[TargetPosition];
        
    }

    public Unit GetUnit(Tile TargetTile)//get the unit in target position
    {

        return GetUnit(TargetTile.position); 

    }

    public Tile GetTile(Vector2Int TargetPosition)//get the tile in target position
    {
        //Debug.Log("GetTile return" + tile.transform.position.ToString() + "    " + TileLayer);
        return init.TileTable[TargetPosition]; 
    }

    public Tile GetTile(Unit TargetUnit)//get the tile in target position
    {
        //Debug.Log("GetTile return" + tile.transform.position.ToString() + "    " + TileLayer);
        return init.TileTable[TargetUnit.position];
    }

    public List<Tile> neighbour(Tile tile)//get all 4 tile close to target tile
    {
        Debug.Log("get into neighbour" + tile.transform.position.ToString());

        List<Tile> neighbours = new List<Tile>();
        if (init.TileTable.ContainsKey(tile.positionUp))
        {
            neighbours.Add(GetTile(tile.positionUp));
        }
        if (init.TileTable.ContainsKey(tile.positionDown))
        {
            neighbours.Add(GetTile(tile.positionDown));
        }
        if (init.TileTable.ContainsKey(tile.positionLeft))
        {
            neighbours.Add(GetTile(tile.positionLeft));
        }
        if (init.TileTable.ContainsKey(tile.positionRight))
        {
            neighbours.Add(GetTile(tile.positionRight));
        }

        Debug.Log("neighbours finish " + neighbours.Count + " tiles within");
        return neighbours;
    }

    /*public void Achievable(Unit unit, Tile tile, int RestMoveable)
    {
        int save = RestMoveable;
        int count = 0;
        //List<Tile> HighLightTiles = new List<Tile>();
        foreach (Tile neighbours in neighbour(tile))//traversal all 4 tile close
        {
            //MoveSpeed = save;
            if (RestMoveable - neighbours.MoveCost(unit.MoveMode) > 0)//if tileCost low than RestMoveable
            {
                if (neighbours.isWalkable == false)//at the same time this tile did not been mark as isWalkable
                {
                    neighbours.Highlight();//highlight tile
                }
                Debug.Log("Tile:  " + neighbours.transform.position.ToString() +
                    "before Achievable:   isWalkable   " + neighbours.isWalkable +
                    ", moveSpeed instant   " + (RestMoveable - neighbours.MoveCost(unit.MoveMode)));
                Achievable(unit, neighbours, RestMoveable -= neighbours.MoveCost(unit.MoveMode));
                RestMoveable = save;//reset the RestMoveable number to initial
                count++;// overflow prevent
                if (count > 50)
                {
                    Debug.Log("stack overflow");
                    return;
                }
            }
            if (neighbours.isWalkable == false && RestMoveable - neighbours.MoveCost(unit.MoveMode) == 0)//if RestMoveable is 0 in this tile
            {
                Debug.Log("Tile:  " + neighbours.transform.position.ToString() +
                   "before Achievable:   isWalkable   " + neighbours.isWalkable +
                   ", moveSpeed instant   " + (RestMoveable - neighbours.MoveCost(unit.MoveMode)));
                neighbours.Highlight();
            }
        }

    }*/

    /* public Dictionary<Vector2Int, int> HighLightTile = new Dictionary<Vector2Int, int>();



    void Search(Unit unit)
    {
        Vector2Int minkey = HighLightTile.Keys.Select(x => new { x, y = HighLightTile[x] }).OrderByDescending(x => x.y).FirstOrDefault().x;
        Achievable(unit, GetTile(minkey), HighLightTile[minkey]);
    }

    public void HighLightTiles(Unit unit, Tile tile, int RestMovable)
    {
        HighLightTile.Clear();


        Achievable(unit, tile, RestMovable);


        foreach (Vector2Int vector in HighLightTile.Keys)
        {
            GetTile(vector).Highlight();
        }
    }

    void Achievable(Unit unit, Tile tile, int RestMovable)
    {
        
        foreach (Tile neighbourTile in neighbour(tile))// traversal the neighbour
        {
            if (RestMovable > neighbourTile.MoveCost(unit.MoveMode))//if the rest movable > the cost of tile, mean is able to achieve
            {

                if (!HighLightTile.ContainsKey(neighbourTile.position))//if the rest movable after move is lower than record before, mean more achievable tile can be found from this tile
                {
                    HighLightTile.Add(neighbourTile.position, RestMovable - neighbourTile.MoveCost(unit.MoveMode));//update the rest movable to large one
                    
                }
                if (RestMovable - neighbourTile.MoveCost(unit.MoveMode) > HighLightTile[neighbourTile.position])
                {
                    HighLightTile.Remove(neighbourTile.position);
                    HighLightTile.Add(neighbourTile.position, RestMovable - neighbourTile.MoveCost(unit.MoveMode));
                }
                if (RestMovable - neighbourTile.MoveCost(unit.MoveMode) <= 0)
                {
                    return;
                }
                Search(unit);

            }
        }
    }*/



    public void Achievable(Unit unit, Tile tile, int RestMoveable)
    {
        List<Vector2Int> HighlightTile = new List<Vector2Int>();
        List<int> TileCost = new List<int>();

        HighlightTile.Clear();
        TileCost.Clear();

        HighlightTile.Add(tile.position);
        TileCost.Add(RestMoveable);

        while (HighlightTile.Count != 0)//keep search neighbour tile
        {
            Tile instant = GetTile(HighlightTile[0]);
            HighlightTile.RemoveAt(0);
            int save = TileCost[0];
            TileCost.RemoveAt(0);

            foreach (Tile neighbourTiles in neighbour(instant))
            {
                //save RestMoveable
                //if neighbourTiles cost lower than RestMoveable, mean it is achievable
                if (neighbourTiles.MoveCost(unit.character.MoveMethod) <= save)
                {
                    if (!HighlightTile.Contains(neighbourTiles.position) || (save - neighbourTiles.MoveCost(unit.character.MoveMethod)) > save)//if neighbourTiles was not be search or the new RestMoveable is higher than previous one
                    {
                        HighlightTile.Add(neighbourTiles.position);
                        TileCost.Add(save - neighbourTiles.MoveCost(unit.character.MoveMethod));//add or reset 
                        neighbourTiles.Highlight();
                        Debug.Log("neighbourTiles position    " + neighbourTiles.position.ToString());
                        Debug.Log("TileCost    " + neighbourTiles.MoveCost(unit.character.MoveMethod));

                        Debug.Log("HighlightTile    " + HighlightTile.Count);
                        Debug.Log("TileCost count    " + TileCost.Count);
                    }
                }
            }
        }
    }
}

