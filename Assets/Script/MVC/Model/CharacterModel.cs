using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : ModelBase
{
    public static CharacterModel instance = new CharacterModel();

    public delegate void CharacterModelUpdateEventHolder(Character character);

    public event CharacterModelUpdateEventHolder CharacterUpdateEvent;

    public event CharacterModelUpdateEventHolder LevelUpEvent;

    public event CharacterModelUpdateEventHolder CharacterDieEvent;

    public void ViewsUpdate(Character character)
    {
        CharacterUpdateEvent?.Invoke(character);
    }

    public void LevelUp(Character character)
    {
        LevelUpEvent?.Invoke(character);
    }

    public void CharacterDie(Character character)
    {
        CharacterDieEvent?.Invoke(character);
    }


    public Character White = new Character(
        0,
        "Alen",
        5,
        5,
        5,
        5,
        5,
        5,
        new Dictionary<WeaponType, float>() { { WeaponType.Implement, 0.3f } },
        new Dictionary<string, float>(),
        OccupationModel.instance.SwordMan,
        new Ability[2]
    );
}