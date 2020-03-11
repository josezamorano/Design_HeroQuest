using HeroQuest.Library.DependencyInjection;
using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace HeroQuest.Library.MovementStrategy
{
    class MovementStrategyInvoker : IMovementStrategyInvoker
    {
        private List<IMovementStrategy> movementStrategies = new List<IMovementStrategy>();
        public MovementStrategyInvoker()
        {
            movementStrategies.Add(ContainerConfig.GetInstance<ILinearStrategy>());
            movementStrategies.Add(ContainerConfig.GetInstance<IZigZagStrategy>());
        }

        public IMovementStrategy GetMovementStrategy(MovementStrategyEnum movementStrategy)
        {
            var movementResult = movementStrategies.Where(a => a.MovementStrategyName == movementStrategy).FirstOrDefault();
            return movementResult;
        }
    }
}
