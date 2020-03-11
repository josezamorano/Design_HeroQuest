using HeroQuest.Library.Enums;

namespace HeroQuest.Library.Interfaces
{
    public interface IHealthState
    {
        HealthStatesEnum HealthStateName { get; }

        int HealthPoints { get; }

        int MovementSpeed { get; }
    }
}
