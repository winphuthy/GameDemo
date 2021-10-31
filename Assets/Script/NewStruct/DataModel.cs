using System.Collections.Generic;

public class WeaponAdvantage
{
    public static Dictionary<MoveMethod, float> LongWeaponMoveMethodAdvantageDictionary;
    public static Dictionary<ArmorType, float> LongWeaponArmorTypeAdvantageDictionary;

    public static Dictionary<MoveMethod, float> ForgingWeaponMoveMethodAdvantageDictionary;
    public static Dictionary<ArmorType, float> ForgingWeaponArmorTypeAdvantageDictionary;

    public static Dictionary<MoveMethod, float> BluntWeaponMoveMethodAdvantageDictionary;
    public static Dictionary<ArmorType, float> BluntWeaponArmorTypeAdvantageDictionary;

    public static Dictionary<MoveMethod, float> RangeWeaponMoveMethodAdvantageDictionary;
    public static Dictionary<ArmorType, float> RangeWeaponArmorTypeAdvantageDictionary;


    public WeaponAdvantage(Dictionary<MoveMethod, float> longWeaponMoveMethodAdvantageDictionary, Dictionary<ArmorType, float> longWeaponArmorTypeAdvantageDictionary, Dictionary<MoveMethod, float> forgingWeaponMoveMethodAdvantageDictionary, Dictionary<ArmorType, float> forgingWeaponArmorTypeAdvantageDictionary, Dictionary<MoveMethod, float> bluntWeaponMoveMethodAdvantageDictionary, Dictionary<ArmorType, float> bluntWeaponArmorTypeAdvantageDictionary, Dictionary<MoveMethod, float> rangeWeaponMoveMethodAdvantageDictionary, Dictionary<ArmorType, float> rangeWeaponArmorTypeAdvantageDictionary)
    {
        LongWeaponMoveMethodAdvantageDictionary = longWeaponMoveMethodAdvantageDictionary;
        LongWeaponArmorTypeAdvantageDictionary = longWeaponArmorTypeAdvantageDictionary;
        ForgingWeaponMoveMethodAdvantageDictionary = forgingWeaponMoveMethodAdvantageDictionary;
        ForgingWeaponArmorTypeAdvantageDictionary = forgingWeaponArmorTypeAdvantageDictionary;
        BluntWeaponMoveMethodAdvantageDictionary = bluntWeaponMoveMethodAdvantageDictionary;
        BluntWeaponArmorTypeAdvantageDictionary = bluntWeaponArmorTypeAdvantageDictionary;
        RangeWeaponMoveMethodAdvantageDictionary = rangeWeaponMoveMethodAdvantageDictionary;
        RangeWeaponArmorTypeAdvantageDictionary = rangeWeaponArmorTypeAdvantageDictionary;
    }
}



