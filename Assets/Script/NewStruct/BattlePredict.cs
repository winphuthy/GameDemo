using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BattlePredict
{
    public Unit Player { get; }
    public Unit Enemy { get; }

    //Ç³¿½±´Character

    public Character PlayerCharacter { get; set; }
    public Character EnemyCharacter { get; set; }

    //ÉËº¦

    public int PlayerPhysicalDamage { get; }
    public int EnemyPhysicalDamage { get; }
    public int PlayerMagicalDamage { get; }
    public int EnemyMagicalDamage { get; }

    public int PlayerPhysicalDefensive { get; }
    public int EnemyPhysicalDefensive { get; }
    public int PlayerMagicalDefensive { get; }
    public int EnemyMagicalDefensive { get; }
    //ÃüÖĞÂÊ

    public float PlayerHitRate { get; }
    public float EnemyHitRate { get; }

    //Ê¹ÓÃÎäÆ÷

    public Weapon PlayerWeapon { get; }
    public Weapon EnemyWeapon { get; }
}