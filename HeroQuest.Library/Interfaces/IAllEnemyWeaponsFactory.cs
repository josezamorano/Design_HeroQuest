using HeroQuest.Library.Enums;

namespace HeroQuest.Library.Interfaces
{
    public interface IAllEnemyWeaponsFactory
    {
        IEnemyWeapon CreateEnemyWeapon(EnemyWeaponsListEnum enemyWeapon);
    }
}
