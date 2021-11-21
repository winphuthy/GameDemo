using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class IAmulet : Item
{
    public int Price { get; }
    public string Path { get; }
    public string Description { get; }

    public abstract void Effect();
}