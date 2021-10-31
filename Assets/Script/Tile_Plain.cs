using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Plain : Tile
{
    public int Onfoot;
    public int RideHorse;
    public int Fly;


    public Hashtable MoveCostMap()
    {
        Hashtable MoveCost = new Hashtable();
        MoveCost.Add("Onfoot", 1);
        MoveCost.Add("RideHorse", 1);
        MoveCost.Add("Fly", 1);
        return MoveCost;
    }
}
