using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;

namespace HeroQuest.Library.PlayerStates
{
    public class InjuredState : IInjuredState
    {
        public HealthStatesEnum HealthStateName { get { return HealthStatesEnum.Injured; } }
        public int HealthPoints { get { return (int)HealthStatesEnum.Injured; } }
        public int MovementSpeed { get { return 1; } }
    }
}
