using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;

namespace HeroQuest.Library.PlayerStates
{
    public class NormalState: INormalState
    {
        public HealthStatesEnum HealthStateName { get { return HealthStatesEnum.Normal; } }
        public int HealthPoints { get { return (int)HealthStatesEnum.Normal; } }
        public int MovementSpeed { get { return 4; }  }
    }
}
