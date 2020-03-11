using HeroQuest.Library.DependencyInjection;
using HeroQuest.Library.Interfaces;
using System.Collections.Generic;

namespace HeroQuest.Library.PlayerStates
{
    public class HealthInvoker: IHealthInvoker
    {
        private List<IHealthState> healthStates = new List<IHealthState>();
        public HealthInvoker()
        {
            healthStates.Add(ContainerConfig.GetInstance<IDeadState>());
            healthStates.Add(ContainerConfig.GetInstance<IInjuredState>());
            healthStates.Add(ContainerConfig.GetInstance<IWeakState>());
            healthStates.Add(ContainerConfig.GetInstance<INormalState>());
            healthStates.Add(ContainerConfig.GetInstance<IStrongState>());
            healthStates.Add(ContainerConfig.GetInstance<ISuperStrongState>());
        }


        public IHealthState GetHealthState(int playerHealthPoints)
        {
            for(var a = 0; a <healthStates.Count-1; a++)
            {
                var healthStateMinPoints = healthStates[a].HealthPoints;
                var healthStateMaxPoints = healthStates[a + 1].HealthPoints;
                if (healthStateMinPoints <= playerHealthPoints && healthStateMaxPoints > playerHealthPoints)
                {
                    return healthStates[a];
                }
            }
            if (playerHealthPoints >= healthStates[healthStates.Count - 1].HealthPoints)
            {
                return healthStates[healthStates.Count - 1];
            }
            return null;
        }
    }
}
