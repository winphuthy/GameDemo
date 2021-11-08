using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Script.NewStruct;

public interface ICondition
{
    bool Activity { get; set; }

    public void Conditon(Character character);
}

public interface IAura
{
    int Duration { get; }

    public IEntity SourceEntity { get; }
}