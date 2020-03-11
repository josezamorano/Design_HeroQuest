using HeroQuest.Library.DependencyInjection;
using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;
using HeroQuest.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace HeroQuest.Library.WeaponStrikeStrategy
{
    public class StrikeStrategyInvoker : IStrikeStrategyInvoker
    {
        List<StrikeStrategyModel> strikeStrategies = new List<StrikeStrategyModel>(); 
        public StrikeStrategyInvoker()
        {
            strikeStrategies.Add(new StrikeStrategyModel()
            {
                StrikeStrategyName = StrikeStrategiesEnum.Swing,
                StrikeStrategyType = ContainerConfig.GetInstance<ISwingStrikeStrategy>()
            } );
            strikeStrategies.Add(new StrikeStrategyModel()
            {
                StrikeStrategyName = StrikeStrategiesEnum.Shoot,
                StrikeStrategyType = ContainerConfig.GetInstance<IStraightShootStrikeStrategy>()
            });
        }      

        public StrikeStrategyModel GetStrikeStrategy(StrikeStrategiesEnum strikeStrategyName)
        {
            var strategy = strikeStrategies.Where(a => a.StrikeStrategyName == strikeStrategyName).FirstOrDefault();
            return strategy;
        }
    }
}
