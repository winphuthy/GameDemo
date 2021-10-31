using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVC
{
    public static MVC instance = new MVC();

    private MVC()
    {
    }

    public Dictionary<string, ViewBase> views = new Dictionary<string, ViewBase>();
    public Dictionary<string, ModelBase> models = new Dictionary<string, ModelBase>();
    public Dictionary<string, Type> ctrls = new Dictionary<string, Type>();

    public void RegisterModel(ModelBase model)
    {
        if (models.ContainsKey(model.Name))
        {
            return;
        }

        models[model.Name] = model;
    }

    public void RegisterController(string name, Type ctrlType)
    {
        if (ctrls.ContainsKey(name))
        {
            return;
        }

        ctrls[name] = ctrlType;
    }

    public void RegisterView(ViewBase view)
    {
        if (views.ContainsKey(view.Name))
        {
            return;
        }

        views[view.Name] = view;

        view.SubscribedEvents = view.GetSubscribedEvent();
    }


    public void SendEvent(string eventName, object eventParam)
    {
        if (ctrls.ContainsKey(eventName))
        {
            Type ctrType = ctrls[eventName];
            var controllerBase = Activator.CreateInstance(ctrType) as ControllerBase;
            controllerBase.Exceute(eventParam);
            return;
        }

        foreach (var view in views)
        {
            if (view.Value.SubscribedEvents.Contains(eventName))
            {
                view.Value.HandleEvents(eventName, eventParam);
            }
        }
    }
}