using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Assets.Script.NewStruct;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEditorInternal;

[Serializable]
public class Character : IEntity
{
    public int ID { get; }
    public string Name { get; }

    private bool isDie;

    public int HP
    {
        get
        {
            if (hp <= 0)
            {
                isDie = true;
                return 0;
            }

            return math.min(hp, MaxHp);
        }
        set { hp = math.min(hp, MaxHp); }
    }

    public int Level { get; private set; }

    private int stamina;

    private int speed;

    private int intelligence;

    private int strength;

    private int fitness;

    private int volition;

    private int hp;

    public int MaxHp
    {
        get { return Stamina * 2; }
        private set { }
    }

    public int MaxExp
    {
        get { return (int) (100 * math.pow(1.25, Level)); }
        private set { }
    }

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
                    buffBonus += buff.EffectValue;
                }
            }

            return (int) (stamina * buffBonus);
        }

        private set { stamina = value; }
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

            return (int) (speed * buffBonus);
        }
        private set { speed = value; }
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

            return (int) (intelligence * buffBonus);
        }
        private set { intelligence = value; }
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

            return (int) (strength * buffBonus);
        }
        private set { strength = value; }
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

            return (int) (fitness * buffBonus);
        }
        private set { fitness = value; }
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

            return (int) (volition * buffBonus);
        }
        private set { volition = value; }
    }


    public Dictionary<string, float> PropertyGrownRateDictionary { get; } // need refactor

    public Dictionary<WeaponType, float> Proficiency { get; private set; } // need refactor

    public Occupation Occupation { get; private set; }

    public Ability[] Abilities { get; set; }

    public MoveMethod MoveMethod { get; }

    public ArmorType ArmorType { get; }

    public int EXP { get; private set; }

    public List<Buff> Buffs;

    public Item[] Items { get; set; }

    [CanBeNull] public Weapon EquippedWeapon { get; private set; }

    public string Description { get; private set; }



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
        Dictionary<WeaponType, float> proficiency,
        Dictionary<string, float> propertyGrownRateDictionary,
        Occupation occupationName,
        Ability[] abilities)
    {
        isDie = false;
        ID = id;
        Name = name;
        EXP = 0;
        MaxExp = 100;
        this.stamina = stamina;
        this.speed = speed;
        this.intelligence = intelligence;
        this.strength = strength;
        this.fitness = fitness;
        this.volition = volition;
        MoveMethod = moveMethod;
        ArmorType = armorType;
        Proficiency = proficiency;
        PropertyGrownRateDictionary = propertyGrownRateDictionary;
        Items = new Item[6];
        Occupation = occupationName;
        Abilities = abilities;
        Description = "";
        Buffs = new List<Buff>();
        WeaponAutoEquip();
    }


    /// <summary>
    /// 增加经验，触发事件，更新界面
    /// increase Exp, trigger event, update relative interface
    /// </summary>
    /// <param name="exp">增加经验的数量</param>
    public void AddExp(int exp)
    {
        if (EXP < 0)
        {
            EXP = 0;
        }

        int restEXP = exp + EXP - ExperienceSystem.MaxEXP(Level);
        if (restEXP >= 0)
        {
            LevelUp();
            AddExp(restEXP);
        }
        else
        {
            EXP += exp;
        }

        CharacterModel.instance.ViewsUpdate(this);
    }


    /// <summary>
    /// 升级，触发事件，更新界面和升级特效
    /// Level up, trigger event, update relative interface and update effect
    /// </summary>
    public void LevelUp()
    {
        throw new System.NotImplementedException();

        CharacterModel.instance.LevelUp(this);
        CharacterModel.instance.ViewsUpdate(this);
    } // TODO: 升级系统


    public Dictionary<string, int> GetAllOriginProperty()
    {
        Dictionary<string, int> originPropertyDictionary = new Dictionary<string, int>();
        originPropertyDictionary.Add("Stamina", Stamina);
        originPropertyDictionary.Add("Speed", Speed);
        originPropertyDictionary.Add("Intelligence", Intelligence);
        originPropertyDictionary.Add("Strength", Strength);
        originPropertyDictionary.Add("Fitness", Fitness);
        originPropertyDictionary.Add("Volition", Volition);
        return originPropertyDictionary;
    }


    /// <summary>
    /// Equip the weapon inside the backpack, trigger event
    /// </summary>
    /// <param name="weapon">the weapon inside the backpack</param>
    public void EquipWeapon(Weapon weapon)
    {
        if (Items.Contains(weapon))
        {
            EquippedWeapon = weapon;
        }
        else throw new Exception("Weapon want be equipped is not in the backpack");

        CharacterModel.instance.ViewsUpdate(this);
    }


    /// <summary>
    /// Automatic equip weapon, usually used in ctor
    /// </summary>
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


    /// <summary>
    /// 
    /// </summary>
    /// <param name="occupationName"></param>
    public void TransferClass(Occupation occupationName)
    {
        if (Occupation.IsTransferable)
        {
            if (Occupation.NextLevelOccupation.Contains(occupationName))
            {
                Occupation = occupationName;
            }
        }

        CharacterModel.instance.ViewsUpdate(this);
    } //TODO: 


    /// <summary>
    /// Write the Description for the Charactor
    /// </summary>
    /// <param name="content">string insert in Description</param>
    public void WriteDescription(string content)
    {
        Description += content;
    }


    /// <summary>
    /// 浅拷贝
    /// </summary>
    /// <returns>浅拷贝副本</returns>
    public Character Clone()
    {
        return MemberwiseClone() as Character;
    }




    public bool LifeCheck()
    {
        if (isDie)
        {
            CharacterModel.instance.CharacterDie(this);
            return false;
        }
        return true;
    }

}