using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface ICondition
{
    bool Activity { get; set; }

    void Condition();
}

public interface IAura
{
    int Duration { get; set; }
}