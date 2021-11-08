using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IConsumables
{
    public int[] Durability { get; }

    public void Use(int wastage);
}