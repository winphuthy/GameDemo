using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEditor.SearchService;
using UnityEngine;

public class Navigation : Singleton<Navigation>
{
    public bool ShowGridNumb;
    private Unit unit;
    private Dictionary<Vector2Int, int> achievableList;
    private List<Node> achievableNodes;
    private List<Node> openArrayList;
    private List<Node> closeArrayList;
    private Vector2Int destionation;
    private readonly Dictionary<Vector2Int, Tile> TileTable;
    private readonly Dictionary<Vector2Int, Unit> UnitTable;

    public GameObject DebugMesh;

    private Navigation()
    {
        TileTable = MapModel.instance.TileTable;
        UnitTable = MapModel.instance.UnitTable;
    }

    public Unit GetUnit(Vector2Int TargetPosition) //get the unit in target position
    {
        //Debug.Log(TargetPosition);
        return UnitTable[TargetPosition];
    }

    public Unit GetUnit(Tile TargetTile) //get the unit in target position
    {
        return GetUnit(TargetTile.position);
    }

    public Tile GetTile(Vector2Int TargetPosition) //get the tile in target position
    {
        //Debug.Log("GetTile return" + tile.transform.position.ToString() + "    " + TileLayer);
        return TileTable[TargetPosition];
    }

    public Tile GetTile(Unit TargetUnit) //get the tile in target position
    {
        //Debug.Log("GetTile return" + tile.transform.position.ToString() + "    " + TileLayer);
        return TileTable[TargetUnit.position];
    }

    public int GetMoveCost(Vector2Int vector2Int)
    {
        return TileTable[vector2Int].MoveCost(unit.character.MoveMethod);
    }

    public List<Vector2Int> neighbour(Vector2Int vector2Int) //get all 4 tile close to target tile
    {
        Tile tile = GetTile(vector2Int);

        Debug.Log("get into neighbour" + tile.transform.localPosition.ToString());

        List<Vector2Int> neighbours = new List<Vector2Int>();
        if (TileTable.ContainsKey(tile.UpTile))
        {
            neighbours.Add(tile.UpTile);
        }

        if (TileTable.ContainsKey(tile.DownTile))
        {
            neighbours.Add(tile.DownTile);
        }

        if (TileTable.ContainsKey(tile.LeftTile))
        {
            neighbours.Add(tile.LeftTile);
        }

        if (TileTable.ContainsKey(tile.RightTile))
        {
            neighbours.Add(tile.RightTile);
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


    /// <summary>
    /// display every achiveable tile
    /// </summary>
    /// <param name="unit">the unity wanna move</param>
    /// <param name="tile"> the tile unit stand on </param>
    /// <param name="RestMoveable"> the range character can arrive</param>
    public void Achievable(Unit unit)
    {
        this.unit = unit;
        destionation = new Vector2Int();
        achievableList = new Dictionary<Vector2Int, int>();
        Queue<Vector2Int> HighlightTile = new Queue<Vector2Int>();
        Queue<int> restMoveableQueue = new Queue<int>();

        HighlightTile.Clear();
        restMoveableQueue.Clear();

        HighlightTile.Enqueue(GetTile(unit).position);
        restMoveableQueue.Enqueue(unit.RestMoveable);
        achievableList[GetTile(unit).position] = unit.RestMoveable;

        while (HighlightTile.Count != 0) //keep search neighbour tile
        {
            Tile instance = GetTile(HighlightTile.Dequeue());
            int restmoveableSave = restMoveableQueue.Dequeue();

            foreach (var neighbourTile in neighbour(instance.position))
            {
                //save RestMoveable
                //if neighbourTiles cost lower than RestMoveable, mean it is achievable

                Tile neighbourTiles = GetTile(neighbourTile);

                if (restmoveableSave - neighbourTiles.MoveCost(unit.character.MoveMethod) >= 0)
                {
                    if (!achievableList.ContainsKey(neighbourTiles.position)
                    ) //if neighbourTiles was not be search or the new RestMoveable is not lower or equal than previous one
                    {
                        HighlightTile.Enqueue(neighbourTiles.position);
                        achievableList[neighbourTiles.position] =
                            restmoveableSave - neighbourTiles.MoveCost(unit.character.MoveMethod);
                        restMoveableQueue.Enqueue(restmoveableSave -
                                                  neighbourTiles.MoveCost(unit.character.MoveMethod)); //add or reset 
                        neighbourTiles.Highlight();

                        Debug.Log("neighbourTiles position    " + neighbourTiles.position.ToString());
                        Debug.Log("TileCost    " + neighbourTiles.MoveCost(unit.character.MoveMethod));

                        Debug.Log("HighlightTile    " + HighlightTile.Count);
                        Debug.Log("TileCost count    " + restMoveableQueue.Count);

                        Debug.Log("position list: " + neighbourTiles.position.ToString());
                        if (ShowGridNumb)
                        {
                            GameObject instanceGO =
                                DebugMesh.transform.Find(
                                    $"text_mesh ({neighbourTiles.position.y * 10 + neighbourTiles.position.x})").gameObject;
                            instanceGO.GetComponent<TextMesh>().text =
                                $"RC: {restmoveableSave - neighbourTiles.MoveCost(unit.character.MoveMethod)}";
                        }
                    }
                }
            }
        }

        Debug.Log("achievableList.account:  " + achievableList.Count);
    }


    /*public void Achievable(Unit unit)
    {
        achievableList = new Dictionary<Tile, int>();// init dictionary
        while (expression)
        {
            
        }
        foreach (var neighbourTile in neighbour(GetTile(unit)))// tourist 4 tile neighbour
        {
            //for each tile neighbour 
            //the present rest movable point is the central tile movable point - present point
            int restMoveAblePresent = neighbourTile.MoveCost(unit.character.MoveMethod);
            if (!achievableList.ContainsKey(neighbourTile) || restMoveAblePresent >= 0)
            {//if this tile did not be in highlight list and have enough point to arrive
                achievableList.Add(neighbourTile, restMoveAblePresent);
                neighbourTile.Highlight();
            }
        }
    }*/


    /// <summary>
    /// Enter the destination, get the path to arrive the destination
    /// </summary>
    /// <param name="vector2">destination</param>
    private class Node : IComparable
    {
        [CanBeNull] public Node Father;
        public int G;
        public int H;
        public int F;
        public Vector2Int Position;


        public Node(Vector2Int Position)
        {
            Father = null;
            this.Position = Position;
            G = 0;
            H = 0;
            F = 0;
        }

        public Node(Node father, Vector2Int position)
        {
            Father = father;
            Position = position;

            G = Father.G + instance.GetMoveCost(position);
            H = math.abs(Position.x - instance.destionation.x) + math.abs(Position.y - instance.destionation.y);
            F = G + H;
        }


        public int CompareTo(object obj)
        {
            Node other = (Node) obj;
            if (F < other.F)
            {
                return -1;
            }
            else if (F > other.F)
            {
                return 1;
            }

            return 0;
        }
    }

    private Node searchNode(Vector2Int vector2Int, List<Node> nodes)
    {
        foreach (Node achievableNode in nodes)
        {
            if (achievableNode.Position == vector2Int)
            {
                return achievableNode;
            }
        }

        return null;
    }

    public List<Vector2Int> GetPathToDestination(Unit unit, Vector2Int destionation)
    {
        achievableNodes = new List<Node>();
        //generate node list for A*
        foreach (var i in achievableList)
        {
            achievableNodes.Add(new Node(i.Key));
        }

        //search for endNode and startNode
        Node endNode = searchNode(destionation, achievableNodes);
        Node startNode = searchNode(unit.position, achievableNodes);

        //check the start and end node has been set
        if (endNode == null || startNode == null)
        {
            throw new Exception("A* error on set start and end node");
        }

        openArrayList = new List<Node>();
        closeArrayList = new List<Node>();

        openArrayList.Add(startNode);

        int count = 0;

        for (;;)
        {
            //safety
            count++;
            if (count >= 1000)
            {
                Debug.Log("stack over flow inside A*");
                break;
            }

            if (openArrayList.Count == 0)
            {
                Debug.Log("Over, no path can achieve destination, something go wrong");
                return null;
            }

            /*foreach (var node in openArrayList)
            {
                if (node.Position == destionation)
                {
                    break;
                }
            }*/

            if (searchNode(destionation, openArrayList) != null)
            {
                Debug.Log("Found!");

                break;
            }

            Node instance = openArrayList[0];

            foreach (var node in neighbour(instance.Position))
            {
                node.ToString();
                Node nextNode = searchNode(node, achievableNodes);

                if (nextNode == null)
                {
                    continue;
                }

                if (achievableNodes.Contains(nextNode) && !closeArrayList.Contains(nextNode))
                {
                    //if instance.g + cost of next node < node.g which has been record in openlist, overwrite it
                    Node recordedNode = null;

                    foreach (Node achievableNode in openArrayList)
                    {
                        if (achievableNode.Position == node)
                        {
                            recordedNode = achievableNode;
                        }
                    }

                    Node newNode = new Node(instance, nextNode.Position);

                    if (recordedNode == null)
                    {
                        openArrayList.Add(newNode);
                    }

                    else if (newNode.G == 0 || recordedNode.G > newNode.G)
                    {
                        recordedNode.G = instance.G + GetMoveCost(nextNode.Position);
                        recordedNode.Father = instance;
                    }
                    /*else
                    {
                        throw new Exception("A* error, get error on update neighbour node");
                    }*/


                    openArrayList.Sort();
                }
            }

            closeArrayList.Add(instance);

            openArrayList.Remove(instance);
        }


        //record Path to List and transform to Tile 
        Node transformNode = searchNode(destionation, openArrayList);
        List<Vector2Int> pathTiles = new List<Vector2Int>();
        int conut = 0;
        do
        {
            conut++;
            if (conut > 1000)
            {
                Debug.Log("stack over flow, transform to tile list error");
                break;
            }

            pathTiles.Add(transformNode.Position);
            transformNode = transformNode.Father;
            transformNode.ToString();
        } while (transformNode.Father != null);

        return pathTiles;
    }
}