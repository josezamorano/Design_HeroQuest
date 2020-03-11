using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;

namespace HeroQuest.Library.PlayerStates
{
    public class SuperStrongState : ISuperStrongState
    {
        public HealthStatesEnum HealthStateName { get { return HealthStatesEnum.SuperStrong; } }
        public int HealthPoints { get { return (int)HealthStatesEnum.SuperStrong; } }
        public int MovementSpeed { get { return 15; } }
    }
}
