using HeroQuest.Library.DependencyInjection;
using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace HeroQuest.Library.GameLevelsTemplate
{
    public class GameLevelInvoker : IGameLevelInvoker
    {
        List<LevelTemplate> allLevelsTemplates = new List<LevelTemplate>();
        public GameLevelInvoker()
        {
            allLevelsTemplates.Add((LevelTemplate)ContainerConfig.GetInstance<ILevel_0_Stop>());
            allLevelsTemplates.Add((LevelTemplate)ContainerConfig.GetInstance<ILevel_1>());
            allLevelsTemplates.Add((LevelTemplate)ContainerConfig.GetInstance<ILevel_2>());
        }

        public LevelTemplate GetLevel(GameLevelsEnum gameLevel)
        {
            var selectedLevel = allLevelsTemplates.Where(a => a.GameLevelNumber == gameLevel).FirstOrDefault();
            return selectedLevel;
        }
    }
}
