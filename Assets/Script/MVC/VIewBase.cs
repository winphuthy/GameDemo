using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ViewBase : MonoBehaviour
{
    public IList<string> SubscribedEvents;

    public virtual string Name
    {
        get
        {
            return GetType().Name;
        }
    }

    public abstract IList<string> GetSubscribedEvent();

    public abstract void HandleEvents(string eventName, object eventParam);
}
