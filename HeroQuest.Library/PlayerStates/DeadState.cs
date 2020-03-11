using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;

namespace HeroQuest.Library.PlayerStates
{
    public class DeadState: IDeadState
    {
        public HealthStatesEnum HealthStateName { get { return HealthStatesEnum.Dead; } }
        public int HealthPoints { get { return (int)HealthStatesEnum.Dead; } }
        public int MovementSpeed { get { return 0; } }
    }
}
