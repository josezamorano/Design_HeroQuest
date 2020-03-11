using HeroQuest.Library.Enums;
using HeroQuest.Library.GameLevelsTemplate;

namespace HeroQuest.Library.Interfaces
{
    public interface IGameLevelInvoker
    {
        LevelTemplate GetLevel(GameLevelsEnum gameLevel);
    }
}
