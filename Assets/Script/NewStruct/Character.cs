using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEditorInternal;

public class Character
{
    public int ID { get; }

    public String Name { get; }

    public int Level { get; private set; }

    /// <summary>
    /// property get method will return the value after buff effect
    /// </summary>
    public int Stamina
    {
        get
        {
            float buffBonus = 0;
            foreach (Buff buff in Buffs)
            {
                if (buff.EffectProperty == "Stamina")
                {
                }

                buffBonus += buff.EffectValue;
            }

            return (int) (Stamina * buffBonus);
        }
        private set { Stamina = value; }
    }

    public int Speed
    {
        get
        {
            float buffBonus = 0;
            foreach (Buff buff in Buffs)
            {
                if (buff.EffectProperty == "Speed")
                {
                }

                buffBonus += buff.EffectValue;
            }

            return (int) (Stamina * buffBonus);
        }
        private set { Speed = value; }
    }

    public int Intelligence
    {
        get
        {
            float buffBonus = 0;
            foreach (Buff buff in Buffs)
            {
                if (buff.EffectProperty == "Intelligence")
                {
                }

                buffBonus += buff.EffectValue;
            }

            return (int) (Stamina * buffBonus);
        }
        private set { Intelligence = value; }
    }

    public int Strength
    {
        get
        {
            float buffBonus = 0;
            foreach (Buff buff in Buffs)
            {
                if (buff.EffectProperty == "Strength")
                {
                }

                buffBonus += buff.EffectValue;
            }

            return (int) (Stamina * buffBonus);
        }
        private set { Strength = value; }
    }

    public int Fitness
    {
        get
        {
            float buffBonus = 0;
            foreach (Buff buff in Buffs)
            {
                if (buff.EffectProperty == "Fitness")
                {
                }

                buffBonus += buff.EffectValue;
            }

            return (int) (Stamina * buffBonus);
        }
        private set { Fitness = value; }
    }

    public int Volition
    {
        get
        {
            float buffBonus = 0;
            foreach (Buff buff in Buffs)
            {
                if (buff.EffectProperty == "Volition")
                {
                }

                buffBonus += buff.EffectValue;
            }

            return (int) (Stamina * buffBonus);
        }
        private set { Volition = value; }
    }

    public Dictionary<String, float> PropertyGrownRateDictionary { get; } // need refactor

    public Dictionary<String, float> Proficiency { get; private set; } // need refactor

    public AbstractClass AbstractClass { get; private set; }

    public Ability[] Abilities { get; set; }

    public MoveMethod MoveMethod { get; }

    public ArmorType ArmorType { get; }

    public int EXP { get; private set; }

    public List<Buff> Buffs;

    public Item[] Items { get; set; }

    public Weapon EquipedWeapon { get; private set; }

    public Character(int id,
        string name,
        int stamina,
        int speed,
        int intelligence,
        int strength,
        int fitness,
        int volition,
        MoveMethod moveMethod,
        ArmorType armorType,
        Dictionary<String, float> proficiency,
        Dictionary<String, float> propertyGrownRateDictionary,
        Item[] items,
        AbstractClass abstractClassName,
        Ability[] abilities)
    {
        ID = id;
        Name = name;
        Stamina = stamina;
        Speed = speed;
        Intelligence = intelligence;
        Strength = strength;
        Fitness = fitness;
        Volition = volition;
        MoveMethod = moveMethod;
        ArmorType = armorType;
        Proficiency = proficiency;
        PropertyGrownRateDictionary = propertyGrownRateDictionary;
        Items = items;
        AbstractClass = abstractClassName;
        Abilities = abilities;
        WeaponAutoEquip();
    }

    public void AddEXP(int exp)
    {
        int restEXP = exp + EXP - ExperienceSystem.MaxEXP(Level);
        if (restEXP >= 0)
        {
            Levelup();
            AddEXP(restEXP);
        }
        else
        {
            EXP += exp;
        }
    }

    /// <summary>
    /// get bool by random in probability from PropertyGrownRateDictionary
    /// </summary>
    public void Levelup()
    {
        throw new System.NotImplementedException();
    }

    public Dictionary<String, int> GetAllOriginProperty()
    {
        Dictionary<String, int> originPropertyDictionary = new Dictionary<string, int>();
        originPropertyDictionary.Add("Stamina", Stamina);
        originPropertyDictionary.Add("Speed", Speed);
        originPropertyDictionary.Add("Intelligence", Intelligence);
        originPropertyDictionary.Add("Strength", Strength);
        originPropertyDictionary.Add("Fitness", Fitness);
        originPropertyDictionary.Add("Volition", Volition);
        return originPropertyDictionary;
    }

    public void EquipWeapon(Weapon weapon)
    {
        EquipedWeapon = weapon;
    }

    public void WeaponAutoEquip()
    {
        foreach (var item in Items)
        {
            if (item is Weapon)
            {
                EquipWeapon(item as Weapon);
                break;
            }
        }
    }

    public void TransferClass(AbstractClass abstractClassName)
    {
        if (AbstractClass.IsTransferable)
        {
            if (AbstractClass.NextLevelClass.Contains(abstractClassName))
            {
                AbstractClass = abstractClassName;
            }
        }
    }
}