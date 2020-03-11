
using HeroQuest.Library.Enums;
using HeroQuest.Library.Models;

namespace HeroQuest.Library.Interfaces
{
    public interface IStrikeStrategyInvoker
    {
        StrikeStrategyModel GetStrikeStrategy(StrikeStrategiesEnum strikeStrategyName);
    }
}
