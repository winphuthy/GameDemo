using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class Occupation
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

    public string ClassName
    {
        get => default;
        set { }
    }


    public string Path
    {
        get => default;
        set { }
    }

    /// <summary>
    /// next level of this Occupation
    /// </summary>
    public Occupation[] NextLevelOccupation
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

    public WeaponType[] EnableWeaponTypes { get; }

    public Occupation(
        string name,
        string path,
        Ability ability,
        MoveMethod moveMethod,
        ArmorType amorArmorType,
        int moveRange,
        WeaponType[] enableWeaponTypes,
        Occupation[] nextLevelOccupation)
    {
        ClassName = name;
        Path = path;
        Ability = ability;
        MoveMethod = moveMethod;
        ArmorType = amorArmorType;
        MoveRange = moveRange;
        EnableWeaponTypes = enableWeaponTypes;
        NextLevelOccupation = nextLevelOccupation;
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