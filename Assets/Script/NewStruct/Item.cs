using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Script.NewStruct;

public interface Item 
{

    public int Price { get; }

    public string Path { get; }

    public string Description { get; }
}