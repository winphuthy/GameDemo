using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanelView : ViewBase
{

    private GameManager gm;

    public override IList<string> GetSubscribedEvent()
    {
        throw new System.NotImplementedException();
    }

    public override void HandleEvents(string eventName, object eventParam)
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        gm = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}