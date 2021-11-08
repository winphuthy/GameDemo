using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : ControllerBase
{

    public event EventHandler MoveEvent;

    public MoveController instance = new MoveController();

    // MoveEventArgs moveEventArgs = new MoveEventArgs();


    public override void Exceute(object param)
    {

        if (param is Character)
        {
            param = param as Character;
        }

        MoveEvent?.Invoke(param, EventArgs.Empty);
    }
}


public class MoveEventArgs : EventArgs
{
    public Tile[] pathTiles;

    public MoveEventArgs(Tile[] pathTiles)
    {
        this.pathTiles = pathTiles;
    }
}