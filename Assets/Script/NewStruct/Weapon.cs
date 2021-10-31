using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class Weapon : Item, IConsumables
{
    /// <summary>
    /// weaponBonus in origin
    /// </summary>
    private float weaponBonus;

    /// <summary>
    /// Range of attack
    /// </summary>
    public int Range
    {
        get => default;
    }

    /// <summary>
    /// input enemy charactor get DamageBonus
    /// </summary>
    public float GetDamageBonus(Character enemy)
    {
        float damageBonus = 0;
        
        if (this is IForgingWeapon)
        {
            foreach (KeyValuePair<ArmorType, float> keyValuePair in WeaponAdvantage.ForgingWeaponArmorTypeAdvantageDictionary)
            {
                if (enemy.ArmorType == keyValuePair.Key)
                {
                    damageBonus += keyValuePair.Value;
                }
            }
            foreach (KeyValuePair<MoveMethod, float> keyValuePair in WeaponAdvantage.ForgingWeaponMoveMethodAdvantageDictionary)
            {
                if (enemy.MoveMethod == keyValuePair.Key)
                {
                    damageBonus += keyValuePair.Value;
                }
            }
        }
        if (this is ILongWeapon)
        {
            foreach (KeyValuePair<ArmorType, float> keyValuePair in WeaponAdvantage.LongWeaponArmorTypeAdvantageDictionary)
            {
                if (enemy.ArmorType == keyValuePair.Key)
                {
                    damageBonus += keyValuePair.Value;
                }
            }
            foreach (KeyValuePair<MoveMethod, float> keyValuePair in WeaponAdvantage.LongWeaponMoveMethodAdvantageDictionary)
            {
                if (enemy.MoveMethod == keyValuePair.Key)
                {
                    damageBonus += keyValuePair.Value;
                }
            }
        }
        if (this is IBluntWeapon)
        {
            foreach (KeyValuePair<ArmorType, float> keyValuePair in WeaponAdvantage.BluntWeaponArmorTypeAdvantageDictionary)
            {
                if (enemy.ArmorType == keyValuePair.Key)
                {
                    damageBonus += keyValuePair.Value;
                }
            }
            foreach (KeyValuePair<MoveMethod, float> keyValuePair in WeaponAdvantage.BluntWeaponMoveMethodAdvantageDictionary)
            {
                if (enemy.MoveMethod == keyValuePair.Key)
                {
                    damageBonus += keyValuePair.Value;
                }
            }
        }

        return damageBonus;
    }

    /// <summary>
    /// input charactor get bool that if charactor can use it
    /// </summary>
    public bool Usable(Character user)
    {
        throw new System.NotImplementedException();
    }

    public int[] Durablilty { get; private set; }
    public void Use()
    {
        throw new NotImplementedException();
    }
}