using HeroQuest.Library.DependencyInjection;
using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;

namespace HeroQuest.Library.WeaponsFactory
{
    public class AllWeaponsFactory: IAllWeaponsFactory
    {
        ISword sword = null;
        IMace mace = null;
        IBowAndArrow bowAndArrow = null;
        
        public IWeapon GetWeapon(WeaponsListEnum weapon)
        {
            switch (weapon)
            {
                case WeaponsListEnum.Sword:
                    sword = sword ?? ContainerConfig.GetInstance<ISword>();
                    return (IWeapon)sword;

                case WeaponsListEnum.Mace:
                    mace = mace ?? ContainerConfig.GetInstance<IMace>();
                    return mace;

                case WeaponsListEnum.Bow:
                    bowAndArrow = bowAndArrow ?? ContainerConfig.GetInstance<IBowAndArrow>();
                    return bowAndArrow;

            }
             return null;
        }
    }
}
