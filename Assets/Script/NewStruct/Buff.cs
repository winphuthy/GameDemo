using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class Buff
{

    public String EffectProperty { get;}

    public float EffectValue { get; set; }

    public Character Source
    {
        get => default;
        set
        {
        }
    }

    public bool Activity
    {
        get => default;
        set
        {
        }
    }
}