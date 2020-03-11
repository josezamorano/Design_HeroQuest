using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;

namespace HeroQuest.Library.PlayerStates
{
    public class StrongState : IStrongState
    {
        public HealthStatesEnum HealthStateName { get { return HealthStatesEnum.Strong; } }
        public int HealthPoints { get { return (int)HealthStatesEnum.Strong; } }
        public int MovementSpeed { get { return 7; } }
    }
}
