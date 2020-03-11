using System.Collections.Generic;

namespace HeroQuest.Library.Interfaces
{
    public interface IWeaponHelper
    {
        bool SetInitialLocation_For_ShootingWeapon(System.Windows.Controls.Image shooterImage, System.Drawing.Point initialLocationPoint, List<System.Windows.Controls.Image> weapon_AllShootingImages);
        void Process_WeaponStraightShootingAction(IShooter shooter, int weaponDamagePower, List<IEnemy> allEnemies, List<System.Windows.Controls.Image> allShootingArrowImages, System.Drawing.Point newPoint);
        void WeaponStrikeTarget(IShooter shooter, int weaponDamagingPoints, System.Windows.Controls.Image weaponImage, List<IEnemy> allActiveEnemies);
    }
}
