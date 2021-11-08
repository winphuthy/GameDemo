using System;

namespace Assets.Script.NewStruct
{
    public interface IPassive//直接影响属性
    {
        public void AddBuff();
    }
    public interface ITriggered
    {
        public void OnTriggered();
    }
    public interface IBattleEffect
    {
        public void Effect(Character character);
    }

}