using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;

namespace HeroQuest.Library.EnemyWeaponsFactory
{
    public class AllEnemyWeaponsFactory : IAllEnemyWeaponsFactory
    {
        IEnemyFireball _enemyFireball = null;

        public IEnemyWeapon CreateEnemyWeapon(EnemyWeaponsListEnum enemyWeapon)
        {
            switch (enemyWeapon)
            {
                case EnemyWeaponsListEnum.Fireball:
                    var enemyFireball = (_enemyFireball == null) ? DependencyInjection.ContainerConfig.GetInstance<IEnemyFireball>(): _enemyFireball;
                    return enemyFireball;
                    
            }
            return null;
        }
    }
}
