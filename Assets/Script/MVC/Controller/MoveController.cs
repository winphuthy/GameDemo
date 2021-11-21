using System;

public class MoveController : ControllerBase
{

    public event EventHandler MoveEvent;

    public static MoveController instance = new MoveController();
    private MoveController(){}


    readonly MoveEventArgs moveEventArgs = new MoveEventArgs(new Tile[]{});//TODO: 完善这个移动系统


    public void Exceute(object param)
    {

        if (param is Unit)
        {
            param = param as Unit;
        }
        else
        {
            throw new Exception("param is not a Unit");
        }

        MoveEvent?.Invoke(param, moveEventArgs);
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