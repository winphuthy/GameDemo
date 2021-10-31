using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IConsumables
{
    int[] Durablilty { get; }

    void Use();
}