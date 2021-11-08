using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhysicalBattlePredict : BattlePredict
{
    public Unit Player { get; }
    public Unit Enemy { get; }
    public Character PlayerCharacter { get; set; }
    public Character EnemyCharacter { get; set; }

    //�����˺�
    public float PlayerWeaponDamageBonus { get; }
    public float EnemyWeaponDamageBonus { get; }

    public int PlayerPhysicalDamage { get; }
    public int EnemyPhysicalDamage { get; }
    public int PlayerMagicalDamage { get; }
    public int EnemyMagicalDamage { get; }
    public int PlayerPhysicalDefensive { get; }
    public int EnemyPhysicalDefensive { get; }
    public int PlayerMagicalDefensive { get; }
    public int EnemyMagicalDefensive { get; }

    public float PlayerHitRate { get; }
    public float EnemyHitRate { get; }
    public Weapon PlayerWeapon { get; set; }
    public Weapon EnemyWeapon { get; }

    private List<Unit> combatProcess;


    /// <summary>
    /// ������������װ������Ϊ������������Ӱ���µ�ս��Ԥ��
    /// </summary>
    /// <param name="player"></param>
    /// <param name="enemy"></param>
    public PhysicalBattlePredict(Unit player, Unit enemy)
    {
        Player = player;
        Enemy = enemy;

        PlayerCharacter = PlayerCharacter.Clone();
        EnemyCharacter = EnemyCharacter.Clone();

        PlayerWeapon = PlayerCharacter.EquippedWeapon;
        EnemyWeapon = EnemyCharacter.EquippedWeapon;

        PlayerWeaponDamageBonus = GetWeaponPhysicalDamageBonus(PlayerWeapon);
        EnemyWeaponDamageBonus = GetWeaponPhysicalDamageBonus(EnemyWeapon);

        PlayerMagicalDamage = 0;
        EnemyMagicalDamage = 0;

        //����������
        PlayerPhysicalDefensive =
            Convert.ToInt16(PlayerCharacter.Stamina + PlayerCharacter.Volition * 0.15 +
                            PlayerCharacter.Strength * 0.05);
        EnemyPhysicalDefensive =
            Convert.ToInt16(EnemyCharacter.Stamina + EnemyCharacter.Volition * 0.15 +
                            EnemyCharacter.Strength * 0.05);

        //ħ������
        PlayerMagicalDefensive =
            Convert.ToInt16(PlayerCharacter.Volition + PlayerCharacter.Stamina +
                            PlayerCharacter.Intelligence * 0.05);
        EnemyMagicalDefensive =
            Convert.ToInt16(EnemyCharacter.Volition + EnemyCharacter.Stamina + EnemyCharacter.Intelligence * 0.05);

        //�����˺�
        PlayerPhysicalDamage =
            Convert.ToInt16(PlayerCharacter.Strength * (1 + PlayerWeaponDamageBonus) - EnemyPhysicalDefensive);
        EnemyPhysicalDamage =
            Convert.ToInt16(PlayerCharacter.Strength * (1 + EnemyWeaponDamageBonus) - PlayerPhysicalDefensive);


        // PlayerHitRate = 1 - (this.Player.Proficiency[PlayerWeapon.WeaponType[0]])

        CombatProcessGeneration();

        WeaponDisadvantage();

        //ս�����ܴ���
        ActiveBattleAbility();
    }


    /// <summary>
    /// ʵ�������Եз��������ʵĶ����˺�
    /// </summary>
    /// <param name="weapon"></param>
    /// <returns>��������ʽ�еĶ����˺��������ֵı���</returns>
    public float GetWeaponPhysicalDamageBonus(Weapon weapon)
    {
        float damageBonus = 0;

        if (weapon.WeaponType.Contains(WeaponType.ForgingWeapon))
        {
            if (EnemyCharacter.ArmorType == ArmorType.NonArmor)
            {
                damageBonus += 0.25f;
            }
            else if (EnemyCharacter.ArmorType == ArmorType.HeavyArmor)
            {
                damageBonus -= 0.25f;
            }
        }

        if (weapon.WeaponType.Contains(WeaponType.BluntWeapon))
        {
            if (EnemyCharacter.ArmorType == ArmorType.HeavyArmor)
            {
                damageBonus += 0.25f;
            }
            else if (EnemyCharacter.ArmorType == ArmorType.LightArmor)
            {
                damageBonus += 0.1f;
            }
        }

        if (weapon.WeaponType.Contains(WeaponType.LongWeapon))
        {
            if (EnemyCharacter.MoveMethod == MoveMethod.Riding)
            {
                damageBonus += 0.25f;
            }
        }

        if (weapon.WeaponType.Contains(WeaponType.RangeWeapon))
        {
            if (EnemyCharacter.MoveMethod == MoveMethod.Flying)
            {
                damageBonus += 0.25f;
            }
        }

        return damageBonus;
    }


    /// <summary>
    /// 
    /// </summary>
    public void WeaponDisadvantage()
    {
    } //todo: ����ȱ��


    /// <summary>
    /// ����ս������
    /// </summary>
    /// <returns>ս������</returns>
    public void CombatProcessGeneration()
    {
        List<Unit> instanceList = new List<Unit>();

        instanceList.Add(Player);

        if (PlayerCharacter.Speed - EnemyCharacter.Speed <= 5)
        {
            instanceList.Add(Enemy);
        }

        if (PlayerCharacter.Speed - EnemyCharacter.Speed > 5 && PlayerCharacter.Speed - EnemyCharacter.Speed <= 10)
        {
            instanceList.Add(Player);
        }

        if (PlayerCharacter.Speed - EnemyCharacter.Speed > 10)
        {
            instanceList.Remove(Player);
        }

        combatProcess = instanceList;
    }


    //TODO: ���㾭��
    public void CombatExecute()
    {
        foreach (var unit in combatProcess)
        {
            if (unit == Player)
            {
                EnemyCharacter.HP -= PlayerPhysicalDefensive - EnemyPhysicalDefensive;
                EnemyCharacter.HP -= PlayerMagicalDamage - EnemyMagicalDefensive;
                if (!EnemyCharacter.LifeCheck())
                {
                    break;
                }
            }
            else
            {
                PlayerCharacter.HP -= EnemyPhysicalDefensive - PlayerPhysicalDefensive;
                PlayerCharacter.HP -= EnemyMagicalDamage - PlayerMagicalDefensive;
                if (!PlayerCharacter.LifeCheck())
                {
                    break;
                }
            }
        }

        // TODO�����㾭��

        Execute();
    }



    //TODO: ��ս�����ܵĵ���
    public void ActiveBattleAbility()
    {
        throw new NotImplementedException();
    }


    //TODO: ִ��ս�����
    public void Execute()
    {

    }
}