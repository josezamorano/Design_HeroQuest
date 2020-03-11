using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroQuest.Library.PlayerStates
{
    public class WeakState : IWeakState
    {
        public HealthStatesEnum HealthStateName { get { return HealthStatesEnum.Weak; } }
        public int HealthPoints { get { return (int)HealthStatesEnum.Weak; } set { } }
        public int MovementSpeed { get { return 2; } set { } }
    }
}
