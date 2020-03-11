using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;

namespace HeroQuest.Library.EnemiesFactory
{
    public class AllEnemiesFactory :IAllEnemiesFactory 
    {
        private IBat _bat = null;
        private IWizard _wizard = null;
        public IEnemy CreateEnemy(EnemyNameEnum enemyName)
        {
            switch (enemyName)
            {
                case EnemyNameEnum.Bat:
                    _bat = (_bat == null) ? DependencyInjection.ContainerConfig.GetInstance<IBat>() : _bat;
                    return _bat;

                case EnemyNameEnum.Ghost: //To be Created
                    return null;

                case EnemyNameEnum.Goul: //To be Created
                    return null;

                case EnemyNameEnum.Wizard:
                    _wizard = (_wizard == null) ? DependencyInjection.ContainerConfig.GetInstance<IWizard>() : _wizard;
                    return _wizard;


            }
            return null;
        }
    }
}
