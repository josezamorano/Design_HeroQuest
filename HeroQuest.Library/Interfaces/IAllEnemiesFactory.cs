using HeroQuest.Library.Enums;

namespace HeroQuest.Library.Interfaces
{
    public interface IAllEnemiesFactory
    {
        IEnemy CreateEnemy(EnemyNameEnum enemyName);
    }
}
