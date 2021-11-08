using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanelView : ViewBase
{

    public Text ExpText;

    public Button AttackButton;

    public void OnAttackButtonClicked()
    {
        print("On Click");
        MVC.instance.SendEvent("AttackEvent",new object());
    }
    public override IList<string> GetSubscribedEvent()
    {
        return new string[]
        {
            "ExpChanged"
        };
    }

    public override void HandleEvents(string eventName, object eventParam)
    {
        switch (eventName)
        {
            case "ExpChanged":
            {
                ExpText.text = eventParam.ToString();
            }
                break;
        }
    }

    void Start()
    {
        //¶©ÔÄÊÂ¼þ
        // gm = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}