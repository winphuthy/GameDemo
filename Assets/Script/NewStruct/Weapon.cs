using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Script.NewStruct;

public class Weapon : Item, IConsumables, IEntity
{
    public int ID { get; }
    public string Name { get; }
    public int Price { get; }
    public string Path { get; }

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
    /// character equip will discount speed, usually negative number
    /// </summary>
    public int SpeedDiscount { get; }



    public int[] Durability { get; }



    public string Description { get; }

    public Weapon(WeaponType[] weaponType, int id, string name, int price, string path, float weaponBonus, int range, int speedDiscount, int[] durability, string description)
    {
        WeaponType = weaponType;
        ID = id;
        Name = name;
        Price = price;
        Path = path;
        WeaponBonus = weaponBonus;
        Range = range;
        SpeedDiscount = speedDiscount;
        Durability = durability;
        Description = description;
    }
    public void Use(int wastage)
    {
        Durability[0] -= wastage;
    }

    /// <summary>
    /// input Enemy charactor get DamageBonus
    /// </summary>
    private void WeaponDisadvantage(BattlePredict battlePredict)
    {
        throw new NotImplementedException();
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
}