using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Script.NewStruct;

public abstract class Weapon : Item, IConsumables, IEntity
{

    /// <summary>
    /// weaponBonus in origin
    /// </summary>
    public float WeaponBonus { get; }

    public WeaponType[] WeaponType;

    /// <summary>
    /// Range of attack
    /// </summary>
    public int Range { get; }

    /// <summary>
    /// input Enemy charactor get DamageBonus
    /// </summary>
    private void WeaponDisadvantage(BattlePredict battlePredict)
    {
        throw new NotImplementedException();
    }

    protected Weapon(float weaponBonus, WeaponType[] weaponType, int range, int[] durability)
    {
        WeaponBonus = weaponBonus;
        WeaponType = weaponType;
        Range = range;
        Durability = durability;
    }

    /// <summary>
    /// 武器是否可用
    /// </summary>
    /// <param name="user">持有者或者使用者</param>
    /// <returns></returns>
    public bool Usable(Character user)
    {
        if (Durability[0] == 0)
        {
            return false;
        }

        foreach (var type in WeaponType)
        {
            if (user.Occupation.EnableWeaponTypes.Contains(type))

                return false;
        }

        return true;
    }

    public int[] Durability { get; }

    public void Use(int wastage)
    {
        Durability[0] -= wastage;
    }

    public int ID { get; }
    public string Name { get; }
    public int Price { get; }
    public int Path { get; }
    public string Description { get; }
}