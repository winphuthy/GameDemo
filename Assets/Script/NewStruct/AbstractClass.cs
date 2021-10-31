using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class AbstractClass
{
    private Ability Ability;

    public MoveMethod MoveMethod
    {
        get => default;
        set { }
    }

    public ArmorType ArmorType
    {
        get => default;
        set { }
    }

    public int MoveRange { get; }

    public String ClassName
    {
        get => default;
        set { }
    }

    public String Path
    {
        get => default;
        set { }
    }

    /// <summary>
    /// next level of this AbstractClass
    /// </summary>
    public AbstractClass[] NextLevelClass
    {
        get => default;
        set { }
    }

    /// <summary>
    /// whether character able to go nextlevel
    /// </summary>
    public bool IsTransferable
    {
        get => default;
        set { }
    }
    
    public AbstractClass(String name,
        String path,
        Ability ability,
        MoveMethod moveMethod,
        ArmorType amorArmorType,
        int moveRange,
        AbstractClass[] nextLevelClass)
    {
        Ability = ability;
        MoveMethod = moveMethod;
        MoveRange = moveRange;
    }

    /// <summary>
    /// input character add ability
    /// </summary>
    public void GetAbility(Character charactor)
    {
        throw new System.NotImplementedException();
    }

    public void Transfer(int ClassNumberSelected)
    {
        throw new System.NotImplementedException();
    }
}