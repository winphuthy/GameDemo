using System;

public abstract class Ability
{
    public string AbilityName;

    public bool activable;
}

// public class myAbility
// {
//     private Ability a = new Ability();
// }


public class SwordsmanAbility : Ability, ITriggered
{
    private Character AbilityOwner;

    public void OnTriggered(object param, EventArgs e)
    {
        //TODO: 
    }

    public SwordsmanAbility(Character abilityOwner)
    {
        AbilityOwner = abilityOwner;
        MoveController.instance.MoveEvent += OnTriggered;
    }
}