using System.Collections;
using System.Collections.Generic;
using Assets.Script.NewStruct;
using UnityEngine;

public class AbilityModel : ModelBase
{
//     public Ability a = new Ability();

    class SwordsmanAbility : Ability, ITriggered
    {


        public void OnTriggered()
        {
            throw new System.NotImplementedException();
        }
    }


}
