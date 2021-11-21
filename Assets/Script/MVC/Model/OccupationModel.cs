using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OccupationModel : ModelBase
{
    public List<Occupation> list = new List<Occupation>();

    public static OccupationModel instance = new OccupationModel();

    private OccupationModel(){}


    public Occupation SwordMan = new Occupation(
        "SwordMan",
        "",
        null,
        MoveMethod.Onfoot,
        ArmorType.NonArmor,
        5,
        new WeaponType[1],
        new Occupation[1]
    );

    public Occupation Warrior = new Occupation(
            "Warrior",
            "",
            null,
            MoveMethod.Onfoot,
            ArmorType.NonArmor,
            5,
            new WeaponType[1],
            new Occupation[1]
        );

}