using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BattlePredict
{
    public Unit Player { get; }
    public Unit Enemy { get; }

    //ǳ����Character

    public Character PlayerCharacter { get; set; }
    public Character EnemyCharacter { get; set; }

    //�˺�

    public int PlayerPhysicalDamage { get; }
    public int EnemyPhysicalDamage { get; }
    public int PlayerMagicalDamage { get; }
    public int EnemyMagicalDamage { get; }

    public int PlayerPhysicalDefensive { get; }
    public int EnemyPhysicalDefensive { get; }
    public int PlayerMagicalDefensive { get; }
    public int EnemyMagicalDefensive { get; }
    //������

    public float PlayerHitRate { get; }
    public float EnemyHitRate { get; }

    //ʹ������

    public Weapon PlayerWeapon { get; }
    public Weapon EnemyWeapon { get; }
}