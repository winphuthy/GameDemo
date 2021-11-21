using System;



/// <summary>
/// 被动技能，直接添加一个buff影响角色属性
/// passive ability directly effect character's property
/// </summary>
public interface IPassive 
{
    public Character AbilityOwner { get; }
    public void AddBuff();
}



/// <summary>
/// 触发技能，关注事件，检查情况，复合条件触发
/// Triggered Ability, subscribe event check condition then active
/// </summary>
public interface ITriggered
{
    public void OnTriggered(Object param, EventArgs e);
}




/// <summary>
///战场技能，影响或者改变战斗预测
/// BattleEffect ability, influence or change the battle Predict
/// </summary>
public interface IBattleEffect
{
    public void Effect(Character character);
}



/// <summary>
///
/// Ring ability, effect character inside the specific rang around the ability owner
/// </summary>
public interface IRing
{
    public Character AbilityOwner { get; }

    public int Range { get; }

    public void Effect();

}