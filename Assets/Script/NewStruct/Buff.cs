using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class Buff
{
    public int ID { get; }
    public string name { get; }

    public string EffectProperty { get; }

    public float EffectValue { get; set; }

}