using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Hill : Tile
{
    public int Onfoot;
    public int RideHorse;
    public int Fly;

    
    public Hashtable MoveCostMap()
    {
        Hashtable MoveCost = new Hashtable();
        MoveCost.Add("Onfoot", Onfoot);
        MoveCost.Add("RideHorse", RideHorse);
        MoveCost.Add("Fly", Fly);
        return MoveCost;
    }
    
}
