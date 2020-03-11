using HeroQuest.Library.Enums;

namespace HeroQuest.Library.Interfaces
{
    public interface IAllWeaponsFactory
    {
        IWeapon GetWeapon(WeaponsListEnum weapon);
    }
}
