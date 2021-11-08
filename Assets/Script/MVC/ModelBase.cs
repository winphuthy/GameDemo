using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModelBase
{
    public virtual string Name
    {
        get
        {
            return GetType().Name;
        }
    }
}
