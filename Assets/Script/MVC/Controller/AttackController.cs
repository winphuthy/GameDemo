using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : ControllerBase
{
    public override void Exceute(object param)
    {
        //提取模型，并且进行战斗流程 event
        PlayerModel mb = MVC.instance.models["PlayerModel"] as PlayerModel;

    }

}

